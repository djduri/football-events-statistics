using FluentAssertions;
using FootballEvents.Domain.Base;
using FootballEvents.Domain.Teams;

namespace FootballEvents.Domain.UnitTests.Teams;

public class TeamTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsEmpty()
    {
        // Given: An empty team name
        // When: Attempting to create a team
        var act = () => Team.Factory.Create(string.Empty);

        // Then: Expecting a domain exception
        act.Should().Throw<DomainException>()
           .Which.ExceptionCode.Should().Be("Team_InvalidName");
    }
}