using FluentAssertions;
using FootballEvents.Domain.Teams;

namespace FootballEvents.Domain.UnitTests.Teams;

public class TeamStatisticsTests
{
    [Fact]
    public void ApplyMatchResult_ShouldAccumulateValuesCorrectly()
    {
        // Given: A team object with default statistics
        var team = Team.Factory.Create("Bayern");

        // When: Updating the match result (e.g., 3:1 win -> 3 points)
        team.ApplyMatchResult(goalsScored: 3, goalsConceded: 1);

        // Then: Verify correct data accumulation within the statistics structure
        team.Statistics.MatchesPlayed.Should().Be(1);
        team.Statistics.GoalScored.Should().Be(3);
        team.Statistics.GoalConceded.Should().Be(1);
        team.Statistics.Points.Should().Be(3);
    }
}