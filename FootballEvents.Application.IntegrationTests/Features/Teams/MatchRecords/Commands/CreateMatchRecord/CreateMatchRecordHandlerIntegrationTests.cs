using FluentAssertions;
using FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
using FootballEvents.Application.IntegrationTests.Common;
using FootballEvents.Application.Services;
using FootballEvents.Domain.Teams;
using FootballEvents.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FootballEvents.Application.IntegrationTests.Features.Teams.MatchRecords.Commands.CreateMatchRecord;

public class CreateMatchRecordHandlerIntegrationTests
{
    [Fact]
    public async Task Handle_ShouldCreateTeamsAndSaveMatchRecordInDatabase()
    {
        // Given: Create an isolated in-memory database context
        using var dbContext = TestDatabaseFactory.CreateInMemoryContext();

        // Initialize repositories, UnitOfWork, and logger
        var matchRecordRepository = new Repository<MatchRecord>(dbContext);
        var teamRepository = new Repository<Team>(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var matchLockService = new MatchLockService();
        var logger = NullLogger<CreateMatchRecordHandler>.Instance;

        var handler = new CreateMatchRecordHandler(matchRecordRepository,
                                                   teamRepository,
                                                   unitOfWork,
                                                   matchLockService,
                                                   logger);

        var command = new CreateMatchRecordCommand("Bayern", "Barcelona", 3, 0);

        // When: Execute the command handler (creates teams and saves the match record)
        var result = await handler.Handle(command, CancellationToken.None);

        // Then: Verify the returned log string matches the specification format
        result.Should().Be("Bayern 1 3 3 0 Barcelona 1 0 0 3");

        // Verify entity state in the in-memory database
        var bayernInDb = await dbContext.Teams.Include(t => t.Statistics)
                                              .FirstOrDefaultAsync(t => t.NormalizedName == "bayern");

        bayernInDb.Should().NotBeNull();
        bayernInDb.Statistics.MatchesPlayed.Should().Be(1);
        bayernInDb.Statistics.Points.Should().Be(3);
        bayernInDb.Statistics.GoalScored.Should().Be(3);
        bayernInDb.Statistics.GoalConceded.Should().Be(0);

        var barcelonaInDb = await dbContext.Teams.Include(t => t.Statistics)
                                                 .FirstOrDefaultAsync(t => t.NormalizedName == "barcelona");

        barcelonaInDb.Should().NotBeNull();
        barcelonaInDb.Statistics.MatchesPlayed.Should().Be(1);
        barcelonaInDb.Statistics.Points.Should().Be(0);

        var matchInDb = await dbContext.MatchRecords.FirstOrDefaultAsync();

        matchInDb.Should().NotBeNull();
        matchInDb.HomeScore.Should().Be(3);
        matchInDb.AwayScore.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ShouldAccumulateStatisticsCorrectlyWhenMultipleMatchesAreProcessed()
    {
        // Given: Create an isolated in-memory database context
        using var dbContext = TestDatabaseFactory.CreateInMemoryContext();

        var matchRecordRepository = new Repository<MatchRecord>(dbContext);
        var teamRepository = new Repository<Team>(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var matchLockService = new MatchLockService();
        var logger = NullLogger<CreateMatchRecordHandler>.Instance;

        var handler = new CreateMatchRecordHandler(matchRecordRepository,
                                                   teamRepository,
                                                   unitOfWork,
                                                   matchLockService,
                                                   logger);

        // Match 1: Bayern vs Barcelona (3:0)
        var command1 = new CreateMatchRecordCommand("Bayern", "Barcelona", 3, 0);
        var result1 = await handler.Handle(command1, CancellationToken.None);
        result1.Should().Be("Bayern 1 3 3 0 Barcelona 1 0 0 3");

        // When: Match 2: PSG vs Bayern (3:3) - Bayern plays away, PSG team is newly created
        var command2 = new CreateMatchRecordCommand("PSG", "Bayern", 3, 3);
        var result2 = await handler.Handle(command2, CancellationToken.None);

        // Then: Verify log output for the second match (Bayern now has 2 matches, 4 points, 6 goals scored, 3 conceded)
        result2.Should().Be("PSG 1 1 3 3 Bayern 2 4 6 3");

        // Verify the final state of Bayern in the database
        var bayernInDb = await dbContext.Teams.Include(t => t.Statistics)
                                              .FirstOrDefaultAsync(t => t.NormalizedName == "bayern");

        bayernInDb.Should().NotBeNull();
        bayernInDb.Statistics.MatchesPlayed.Should().Be(2);
        bayernInDb.Statistics.Points.Should().Be(4); // 3 (against Barcelona) + 1 (against PSG)
        bayernInDb.Statistics.GoalScored.Should().Be(6); // 3 + 3
        bayernInDb.Statistics.GoalConceded.Should().Be(0 + 3);

        // Verify that the Bayern team record was not duplicated in the database
        var totalBayernCount = await dbContext.Teams.CountAsync(t => t.NormalizedName == "bayern");
        totalBayernCount.Should().Be(1);
    }
}