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
        // Given: Pusta lista meczów dla dowolnego ID drużyny
        var matches = new List<MatchRecord>();

        // When: Wywołanie kalkulatora
        var result = TeamStatisticsCalculator.Calculate(1, matches);

        // Then: Wszystkie statystyki powinny być zerowe
        result.MatchesPlayed.Should().Be(0);
        result.Points.Should().Be(0);
        result.GoalsScored.Should().Be(0);
        result.GoalsConceded.Should().Be(0);
        result.Form.Should().BeEmpty();
        result.AverageGoals.Should().Be(0);
    }
}