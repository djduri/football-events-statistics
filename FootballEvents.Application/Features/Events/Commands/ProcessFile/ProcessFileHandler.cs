using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
using FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;
using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Logging; 

namespace FootballEvents.Application.Features.Events.Commands.ProcessFile;

internal sealed class ProcessFileHandler : ICommandHandler<ProcessFileCommand, long>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProcessFileHandler> _logger;

    public ProcessFileHandler(IMediator mediator, ILogger<ProcessFileHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<long> Handle(ProcessFileCommand request, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(request.FileStream);
        long processedLines = 0;
        string? line;

        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            try
            {
                using var document = JsonDocument.Parse(line);
                var root = document.RootElement;

                if (root.TryGetProperty("home_team", out _))
                {
                    var homeTeam = root.GetProperty("home_team").GetString();
                    var awayTeam = root.GetProperty("away_team").GetString();

                    if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
                    {
                        _logger.LogWarning("Invalid match payload structure on line: {Line}", line);
                        continue;
                    }

                    var homeScore = root.GetProperty("home_score").GetInt32();
                    var awayScore = root.GetProperty("away_score").GetInt32();

                    var command = new CreateMatchRecordCommand(homeTeam, awayTeam, homeScore, awayScore);
                    await _mediator.Send(command, cancellationToken);
                }
                else if (root.TryGetProperty("teams", out var teamsElement))
                {
                    var teamsList = teamsElement.EnumerateArray()
                        .Select(t => t.GetString())
                        .Where(t => !string.IsNullOrEmpty(t))
                        .Cast<string>()
                        .ToList();

                    if (!teamsList.Any())
                    {
                        _logger.LogWarning("Empty teams list in statistics request on line: {Line}", line);
                        continue;
                    }

                    var query = new GetTeamsStatisticsQuery(teamsList);
                    await _mediator.Send(query, cancellationToken);
                }
                else
                {
                    _logger.LogWarning("Unknown message format encountered on line: {Line}", line);
                }

                processedLines++;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to parse JSON line: {Line}", line);
            }
        }

        return processedLines;
    }
}