using FluentAssertions;
using FootballEvents.Domain.Base;
using FootballEvents.Domain.Teams;

namespace FootballEvents.Domain.UnitTests.Teams;

public class TeamTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsEmpty()
    {
        // Given / When: Próba utworzenia drużyny z pustą nazwą
        var act = () => Team.Factory.Create(string.Empty);

        // Then: Oczekujemy błędu domeny z pełnym kodem zgodnym z konwencją
        act.Should().Throw<DomainException>()
           .Which.ExceptionCode.Should().Be("Team_InvalidName");
    }
}