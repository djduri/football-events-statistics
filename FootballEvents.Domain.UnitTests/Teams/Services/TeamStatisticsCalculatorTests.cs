using Xunit;
using FluentAssertions;
using FootballEvents.Domain.Teams.Services;
using FootballEvents.Domain.Teams;

namespace FootballEvents.Domain.UnitTests.Teams.Services;

public class TeamStatisticsCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnZeroesWhenMatchListIsEmpty()
    {
        // Given: An empty match list for any team ID
        var matches = new List<MatchRecord>();

        // When: Invoking the calculator
        var result = TeamStatisticsCalculator.Calculate(1, matches);

        // Then: All statistics should be zero
        result.MatchesPlayed.Should().Be(0);
        result.Points.Should().Be(0);
        result.GoalsScored.Should().Be(0);
        result.GoalsConceded.Should().Be(0);
        result.Form.Should().BeEmpty();
        result.AverageGoals.Should().Be(0);
    }
}