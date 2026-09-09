namespace FootballEvents.Domain.Teams;
public sealed class MatchRecord
{
    public long Id { get; private init; }
    public long HomeTeamId { get; private set; }
    public Team HomeTeam { get; private set; }
    public long AwayTeamId { get; private set; }
    public Team AwayTeam { get; private set; }
    public int HomeScore { get; private set; }
    public int AwayScore { get; private set; }
    public DateTime MatchDate { get; private set; }

    internal MatchRecord()
    {
        HomeScore = 0;
        AwayScore = 0;
        MatchDate = DateTime.UtcNow;
    }

    public static class Factory
    {
        public static MatchRecord Create(long homeTeamId, long awayTeamId, int homeScore, int awayScore, DateTime matchDate)
        {
            return new MatchRecord()
            {
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                HomeScore = homeScore,
                AwayScore = awayScore,
                MatchDate = matchDate
            };
        }
    }
}
