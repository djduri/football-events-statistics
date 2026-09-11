using FootballEvents.Domain.Base;
using FootballEvents.Domain.Extensions;
using FootballEvents.Domain.Messages;
using FootballEvents.Domain.Teams.Services;

namespace FootballEvents.Domain.Teams;
public sealed class Team : Entity
{
    public string Name { get; private set; }
    public string NormalizedName { get; private set; }
    public TeamStatistics Statistics { get; private set; } = null!;

    public void ApplyMatchResult(int goalsScored, int goalsConceded)
    {
        int points = MatchRules.CalculatePoints(goalsScored, goalsConceded);
        Statistics.Update(goalsScored, goalsConceded, points);
    }

    public Team SetName(string name)
    {
        if (name.IsEmpty())
            throw DomainException.FromErrorCode(ErrorCodes.Team.InvalidName);

        Name = name;
        NormalizedName = name.ToNormalizedKey();
        return this;
    }

    internal Team()
    {
        Name = string.Empty;
        NormalizedName = string.Empty;
        Statistics = new TeamStatistics();
    }

    public static class Factory
    {
        public static Team Create(string name)
        {
            return new Team()
                .SetName(name);
        }
    }
}
