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
        // Given: Ta sama drużyna jako gospodarz i gość
        var team = Team.Factory.Create("Bayern");

        // When: Próba utworzenia meczu
        var act = () => MatchRecord.Factory.Create(team, team, 2, 1, DateTime.UtcNow);

        // Then: Oczekujemy błędu domeny
        act.Should().Throw<DomainException>()
           .Which.ExceptionCode.Should().Be("MatchRecord_HomeTeamAndAwayTeamCannotBeTheSame");
    }
}
