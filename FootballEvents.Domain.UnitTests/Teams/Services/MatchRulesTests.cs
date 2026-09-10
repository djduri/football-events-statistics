using FluentAssertions;
using FootballEvents.Domain.Teams.Services;

namespace FootballEvents.Domain.UnitTests.Teams.Services;

public class MatchRulesTests
{
    [Theory]
    [InlineData(3, 0, 3)]
    [InlineData(1, 1, 1)]
    [InlineData(0, 2, 0)]
    public void CalculatePoints_ShouldReturnExpectedPoints(int scored, int conceded, int expectedPoints)
    {
        var points = MatchRules.CalculatePoints(scored, conceded);
        points.Should().Be(expectedPoints);
    }

    [Theory]
    [InlineData(2, 1, 'W')]
    [InlineData(1, 1, 'D')]
    [InlineData(0, 1, 'L')]
    public void CalculateOutcomeChar_ShouldReturnExpectedChar(int scored, int conceded, char expectedChar)
    {
        var outcome = MatchRules.CalculateOutcomeChar(scored, conceded);
        outcome.Should().Be(expectedChar);
    }
}