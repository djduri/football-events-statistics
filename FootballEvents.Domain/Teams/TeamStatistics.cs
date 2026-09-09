namespace FootballEvents.Domain.Teams;
public sealed class TeamStatistics
{
    public long TeamId { get; private set; }
    public Team Team { get; private set; }
    public int MatchesPlayed { get; private set; }
    public int Points { get; private set; }
    public int GoalScored { get; private set; }
    public int GoalConceded { get; private set; }

    internal void Update(int goalsScored, int goalsConceded, int pointsEarned)
    {
        MatchesPlayed++;
        Points += pointsEarned;
        GoalScored += goalsScored;
        GoalConceded += goalsConceded;
    }

    internal TeamStatistics()
    {
        MatchesPlayed = 0;
        Points = 0;
        GoalScored = 0;
        GoalConceded = 0;
    }

}
