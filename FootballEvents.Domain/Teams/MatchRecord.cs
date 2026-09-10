using FootballEvents.Domain.Base;
using FootballEvents.Domain.Messages;

namespace FootballEvents.Domain.Teams;
public sealed class MatchRecord : Entity
{
    public long HomeTeamId { get; private set; }
    public Team HomeTeam { get; private set; }
    public long AwayTeamId { get; private set; }
    public Team AwayTeam { get; private set; }
    public int HomeScore { get; private set; }
    public int AwayScore { get; private set; }

    internal MatchRecord()
    {
        HomeScore = 0;
        AwayScore = 0;
    }

    public static class Factory
    {
        public static MatchRecord Create(Team homeTeam, Team awayTeam, int homeScore, int awayScore)
        {
            if (homeTeam.NormalizedName == awayTeam.NormalizedName)            
                throw DomainException.FromErrorCode(ErrorCodes.MatchRecord.HomeTeamAndAwayTeamCannotBeTheSame);         
            
            return new MatchRecord()
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                HomeScore = homeScore,
                AwayScore = awayScore
            };
        }
    }
}
