using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Features.Teams.TeamStatistics.DTOs;
using FootballEvents.Domain.Extensions;
using FootballEvents.Domain.Teams.Services;
using FootballEvents.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;

internal sealed class GetTeamsStatisticsHandler : IQueryHandler<GetTeamsStatisticsQuery, List<TeamStatisticDto>>
{
    private readonly DatabaseContext _dbContext;
    private readonly ILogger<GetTeamsStatisticsHandler> _logger;

    // Defines the rolling window size for recent match statistics
    private const int RecentMatchesCount = 3;

    public GetTeamsStatisticsHandler(DatabaseContext dbContext, ILogger<GetTeamsStatisticsHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<TeamStatisticDto>> Handle(GetTeamsStatisticsQuery request, CancellationToken cancellationToken)
    {
        // Normalize requested team names to ensure case-insensitive matching
        var normalizedNames = request.Teams.Select(t => t.ToNormalizedKey()).ToList();

        // Retrieve all matching team entities from the database in a single query
        var teams = await _dbContext.Teams
            .Where(t => normalizedNames.Contains(t.NormalizedName))
            .ToListAsync(cancellationToken);

        var resultList = new List<TeamStatisticDto>();
        var logParts = new List<string>();

        // Iterate over the requested teams to preserve the specific order requested by the client
        foreach (var requestedTeamName in request.Teams)
        {
            var team = teams.FirstOrDefault(t => t.NormalizedName == requestedTeamName.ToNormalizedKey());
            if (team is null)
            {
                // If the team does not exist in the database, return default zero-filled statistics to preserve the response order
                resultList.Add(new TeamStatisticDto
                {
                    Name = requestedTeamName,
                    Form = string.Empty,
                    AverageGoals = 0,
                    MatchesPlayed = 0,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                });

                logParts.Add($"{requestedTeamName}  0 0 0 0 0");

                continue;
            }

            // Fetch the recent matches for the team, ordered by ID descending to strictly follow insertion history
            var recentMatches = await _dbContext.MatchRecords
                .Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)
                .OrderByDescending(m => m.Id)
                .Take(RecentMatchesCount)
                .ToListAsync(cancellationToken);

            // Compute rolling statistics utilizing the domain service calculator
            var stats = TeamStatisticsCalculator.Calculate(team.Id, recentMatches);

            // Build the required raw text log segment for this team
            logParts.Add($"{team.Name} {stats.Form} {stats.FormattedAverageGoals} {stats.MatchesPlayed} {stats.Points} {stats.GoalsScored} {stats.GoalsConceded}");

            // Map the computed statistics to a structured DTO for the API response
            resultList.Add(new TeamStatisticDto
            {
                Name = team.Name,
                Form = stats.Form,
                AverageGoals = stats.AverageGoals,
                MatchesPlayed = stats.MatchesPlayed,
                Points = stats.Points,
                GoalsScored = stats.GoalsScored,
                GoalsConceded = stats.GoalsConceded
            });
        }

        // Write the combined raw format log output to the logging provider if any records were processed
        if (logParts.Any())        
            _logger.LogInformation("{StatsOutput}", string.Join(" ", logParts));        

        // Return the structured DTO list for clean JSON serialization in the API response
        return resultList;
    }
}