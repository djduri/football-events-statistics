using FluentAssertions;
using FootballEvents.Application.Features.Events.Commands.ProcessFile;
using FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
using FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Text;

namespace FootballEvents.Application.IntegrationTests.Features.Events.Commands.ProcessFile;

public class ProcessFileHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProcessFileHandler _handler;

    public ProcessFileHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        // Using NullLogger to avoid mocking issues with internal classes
        var logger = NullLogger<ProcessFileHandler>.Instance;
        _handler = new ProcessFileHandler(_mediatorMock.Object, logger);
    }

    [Fact]
    public async Task Handle_ValidFileContent_ProcessesAllLinesAndReturnsCorrectCount()
    {
        // Given: Valid file content with a match record, an empty line, and a statistics query
        var fileContent = "{\"home_team\": \"Bayern\", \"away_team\": \"Barcelona\", \"home_score\": 3, \"away_score\": 0}\n" +
                          "\n" + // Empty line to be skipped
                          "{\"teams\": [\"Bayern\"]}";

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
        var command = new ProcessFileCommand(stream);

        // When: Handling the file processing command
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then: Exactly 2 valid lines should be processed (empty line ignored)
        result.Should().Be(2);

        // Verify that CreateMatchRecordCommand was sent for the match record
        _mediatorMock.Verify(m => m.Send(It.Is<CreateMatchRecordCommand>(c => c.HomeTeam == "Bayern" && c.AwayTeam == "Barcelona" && c.HomeScore == 3 && c.AwayScore == 0),
                                         It.IsAny<CancellationToken>()), Times.Once);

        // Verify that GetTeamsStatisticsQuery was sent for the statistics request
        _mediatorMock.Verify(m => m.Send(It.Is<GetTeamsStatisticsQuery>(q => q.Teams.Contains("Bayern")),It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidJsonLine_LogsErrorAndContinuesProcessing()
    {
        // Given: File content containing a match record, invalid JSON payload, and a statistics query
        var fileContent =
            "{\"home_team\": \"Bayern\", \"away_team\": \"Real\", \"home_score\": 1, \"away_score\": 1}\n" +
            "INVALID_JSON_GARBAGE_LINE\n" + // Corrupted/invalid JSON line
            "{\"teams\": [\"Real\"]}";

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
        var command = new ProcessFileCommand(stream);

        // When: Handling the file processing command with corrupted data
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then: 2 valid lines processed successfully while ignoring the corrupted line in the counter
        result.Should().Be(2);

        // Ensure valid commands and queries were still successfully dispatched via Mediator
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateMatchRecordCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetTeamsStatisticsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}