using FluentAssertions;
using FootballEvents.Domain.Teams;

namespace FootballEvents.Domain.UnitTests.Teams;

public class TeamStatisticsTests
{
    [Fact]
    public void ApplyMatchResult_ShouldAccumulateValuesCorrectly()
    {
        // Given: Obiekt drużyny z domyślnymi statystykami
        var team = Team.Factory.Create("Bayern");

        // When: Aktualizujemy wynik meczu (np. wygrana 3:1 -> 3 punkty)
        team.ApplyMatchResult(goalsScored: 3, goalsConceded: 1);

        // Then: Weryfikacja poprawności kumulacji danych w strukturze statystyk
        team.Statistics.MatchesPlayed.Should().Be(1);
        team.Statistics.GoalScored.Should().Be(3);
        team.Statistics.GoalConceded.Should().Be(1);
        team.Statistics.Points.Should().Be(3);
    }
}