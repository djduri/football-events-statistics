using FluentAssertions;
using FootballEvents.Domain.Base;
using FootballEvents.Domain.Messages;
using FootballEvents.Domain.Teams;

namespace FootballEvents.Domain.UnitTests.Teams;

public class MatchRecordTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenHomeAndAwayTeamAreTheSame()
    {
        // Given: The same team as both home and away
        var team = Team.Factory.Create("Bayern");

        // When: Attempting to create a match
        var act = () => MatchRecord.Factory.Create(team, team, 2, 1);

        // Then: Expecting a domain exception
        act.Should().Throw<DomainException>()
           .Which.ExceptionCode.Should().Be("MatchRecord_HomeTeamAndAwayTeamCannotBeTheSame");
    }
}