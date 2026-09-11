using FluentAssertions;
using FootballEvents.Domain.Teams.Services;

namespace FootballEvents.Domain.UnitTests.Teams.Services;

public class MatchRulesTests
{
    [Theory]
    [InlineData(3, 0, MatchRules.WinPoints)]
    [InlineData(1, 1, MatchRules.DrawPoints)]
    [InlineData(0, 2, MatchRules.LossPoints)]
    public void CalculatePoints_ShouldReturnExpectedPoints(int scored, int conceded, int expectedPoints)
    {
        var points = MatchRules.CalculatePoints(scored, conceded);
        points.Should().Be(expectedPoints);
    }

    [Theory]
    [InlineData(2, 1, MatchRules.WinChar)]
    [InlineData(1, 1, MatchRules.DrawChar)]
    [InlineData(0, 1, MatchRules.LossChar)]
    public void CalculateOutcomeChar_ShouldReturnExpectedChar(int scored, int conceded, char expectedChar)
    {
        var outcome = MatchRules.CalculateOutcomeChar(scored, conceded);
        outcome.Should().Be(expectedChar);
    }
}