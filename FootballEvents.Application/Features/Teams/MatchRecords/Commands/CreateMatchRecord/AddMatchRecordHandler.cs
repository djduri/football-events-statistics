using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Features.Teams.Teams.Specifications;
using FootballEvents.Application.Services.Interfaces;
using FootballEvents.Domain.Teams;
using FootballEvents.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

namespace FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;

internal sealed class CreateMatchRecordHandler : ICommandHandler<CreateMatchRecordCommand, string>
{
    private readonly IRepository<MatchRecord> _matchRecordRepository;
    private readonly IRepository<Team> _teamRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatchLockService _matchLockService;
    private readonly ILogger<CreateMatchRecordHandler> _logger;

    public CreateMatchRecordHandler(IRepository<MatchRecord> matchRecordRepository,
                                    IRepository<Team> teamRepository,
                                    IUnitOfWork unitOfWork,
                                    IMatchLockService matchLockService,
                                    ILogger<CreateMatchRecordHandler> logger)
    {
        _matchRecordRepository = matchRecordRepository;
        _teamRepository = teamRepository;
        _unitOfWork = unitOfWork;
        _matchLockService = matchLockService;
        _logger = logger;
    }

    public async Task<string> Handle(CreateMatchRecordCommand request, CancellationToken cancellationToken)
    {
        using var releaser = await _matchLockService.LockAsync(cancellationToken);

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            // Resolve or create participating teams
            var homeTeam = await GetOrCreateTeamAsync(request.HomeTeam, cancellationToken);
            var awayTeam = await GetOrCreateTeamAsync(request.AwayTeam, cancellationToken);

            // Create and register the new match record
            var newMatchRecord = MatchRecord.Factory.Create(
                homeTeam: homeTeam,
                awayTeam: awayTeam,
                homeScore: request.HomeScore,
                awayScore: request.AwayScore
            );
            _matchRecordRepository.Add(newMatchRecord);

            // Apply match results to update cumulative statistics
            homeTeam.ApplyMatchResult(request.HomeScore, request.AwayScore);
            awayTeam.ApplyMatchResult(request.AwayScore, request.HomeScore);

            // Commit all modifications within a single database transaction
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            // Build the simplified log output string required by the specification
            var statsOutput = $"{homeTeam.Name} {homeTeam.Statistics.MatchesPlayed} {homeTeam.Statistics.Points} {homeTeam.Statistics.GoalScored} {homeTeam.Statistics.GoalConceded} " +
                          $"{awayTeam.Name} {awayTeam.Statistics.MatchesPlayed} {awayTeam.Statistics.Points} {awayTeam.Statistics.GoalScored} {awayTeam.Statistics.GoalConceded}";

            _logger.LogInformation("{StatsOutput}", statsOutput);

            return statsOutput;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the match record for home team {HomeTeam} and away team {AwayTeam}", request.HomeTeam, request.AwayTeam);
            await transaction.RollbackAsync(cancellationToken);  
            throw;
        }
    }

    private async Task<Team> GetOrCreateTeamAsync(string teamName, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetAsync(new FindTeamByNameSpecification(teamName), cancellationToken);

        if (team is not null)
            return team;

        team = Team.Factory.Create(teamName);
        _teamRepository.Add(team);

        return team;
    }
}