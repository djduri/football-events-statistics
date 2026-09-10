using FluentAssertions;
using FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;
using FootballEvents.Application.IntegrationTests.Common;
using FootballEvents.Application.Services;
using FootballEvents.Domain.Teams;
using Microsoft.Extensions.Logging.Abstractions;

namespace FootballEvents.Application.IntegrationTests.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;

public class GetTeamsStatisticsHandlerIntegrationTests
{
    [Fact]
    public async Task Handle_ShouldReturnStatisticsInRequestedOrderAndRespectRollingWindow()
    {
        // Given: Create an isolated in-memory database context
        using var dbContext = TestDatabaseFactory.CreateInMemoryContext();

        // Create teams
        var bayern = Team.Factory.Create("Bayern");
        var barcelona = Team.Factory.Create("Barcelona");
        var real = Team.Factory.Create("Real");
        var milan = Team.Factory.Create("Milan");
        var psg = Team.Factory.Create("PSG");

        dbContext.Teams.AddRange(bayern, barcelona, real, milan, psg);
        await dbContext.SaveChangesAsync();

        // Add historical match records for Bayern (following the specification sequence)
        dbContext.MatchRecords.Add(MatchRecord.Factory.Create(bayern, barcelona, 3, 0));
        dbContext.MatchRecords.Add(MatchRecord.Factory.Create(psg, bayern, 3, 3));
        dbContext.MatchRecords.Add(MatchRecord.Factory.Create(bayern, real, 0, 1));
        dbContext.MatchRecords.Add(MatchRecord.Factory.Create(milan, bayern, 1, 3));
        await dbContext.SaveChangesAsync();

        var matchLockService = new MatchLockService();
        var handler = new GetTeamsStatisticsHandler(dbContext, matchLockService, NullLogger<GetTeamsStatisticsHandler>.Instance);

        // Request statistics in a specific order: Bayern first, then Milan
        var query = new GetTeamsStatisticsQuery(new List<string> { "Bayern", "Milan" });

        // When: Execute the query handler
        var result = await handler.Handle(query, CancellationToken.None);

        // Then: Verify that the requested order is preserved
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("Bayern");
        result[1].Name.Should().Be("Milan");

        // Verify calculation correctness for Bayern (restricted to the 3-match rolling window)
        var bayernStats = result[0];
        bayernStats.Form.Should().Be("WLD");
        bayernStats.AverageGoals.Should().Be(3.67);
        bayernStats.MatchesPlayed.Should().Be(3);
        bayernStats.Points.Should().Be(4);
        bayernStats.GoalsScored.Should().Be(6);
        bayernStats.GoalsConceded.Should().Be(5);

        // Verify calculation correctness for Milan
        var milanStats = result[1];
        milanStats.Form.Should().Be("L");
        milanStats.AverageGoals.Should().Be(4.0);
        milanStats.MatchesPlayed.Should().Be(1);
        milanStats.Points.Should().Be(0);
        milanStats.GoalsScored.Should().Be(1);
        milanStats.GoalsConceded.Should().Be(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnDefaultZeroStatisticsWhenTeamDoesNotExist()
    {
        // Given: Create an isolated in-memory database context with no teams
        using var dbContext = TestDatabaseFactory.CreateInMemoryContext();

        var matchLockService = new MatchLockService();
        var handler = new GetTeamsStatisticsHandler(dbContext, matchLockService, NullLogger<GetTeamsStatisticsHandler>.Instance);

        // Request statistics for a team that does not exist in the database
        var query = new GetTeamsStatisticsQuery(new List<string> { "NonExistentTeam" });

        // When: Execute the query handler
        var result = await handler.Handle(query, CancellationToken.None);

        // Then: Verify that default zero-filled statistics are returned
        result.Should().HaveCount(1);
        result[0].Name.Should().Be("NonExistentTeam");
        result[0].MatchesPlayed.Should().Be(0);
        result[0].Points.Should().Be(0);
        result[0].GoalsScored.Should().Be(0);
        result[0].GoalsConceded.Should().Be(0);
        result[0].AverageGoals.Should().Be(0);
        result[0].Form.Should().BeEmpty();
    }
}