using System.Globalization;
using System.Text;

namespace FootballEvents.Domain.Teams.Services;

/// <summary>
/// Domain service responsible for calculating rolling match statistics and form for a team.
/// </summary>
public static class TeamStatisticsCalculator
{
    /// <summary>
    /// Represents the calculated statistical result for a team over a given window of matches.
    /// </summary>
    public sealed record Result(
        string Form,
        double AverageGoals,
        string FormattedAverageGoals,
        int MatchesPlayed,
        int Points,
        int GoalsScored,
        int GoalsConceded
    );

    /// <summary>
    /// Computes rolling statistics and form based on a collection of recent match records.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team being evaluated.</param>
    /// <param name="matches">The list of recent matches involving the team.</param>
    /// <returns>A <see cref="Result"/> object containing all aggregated metrics.</returns>
    public static Result Calculate(long teamId, List<MatchRecord> matches)
    {
        int matchesPlayed = 0;
        int points = 0;
        int goalsScored = 0;
        int goalsConceded = 0;
        var formBuilder = new StringBuilder();

        // Iterate through the recent matches to accumulate totals and form sequence
        foreach (var match in matches)
        {
            matchesPlayed++;
            bool isHome = match.HomeTeamId == teamId;

            // Extract goals scored and conceded depending on whether the team played home or away
            int scored = isHome ? match.HomeScore : match.AwayScore;
            int conceded = isHome ? match.AwayScore : match.HomeScore;

            goalsScored += scored;
            goalsConceded += conceded;

            // Evaluate match outcome: Win (3 pts, 'W'), Draw (1 pt, 'D'), or Loss (0 pts, 'L')
            if (scored > conceded)
            {
                points += 3;
                formBuilder.Append('W');
            }
            else if (scored == conceded)
            {
                points += 1;
                formBuilder.Append('D');
            }
            else
            {
                formBuilder.Append('L');
            }
        }

        // Calculate average total goals per match, handling division by zero safety
        double avg = matchesPlayed == 0 ? 0.0 : (double)(goalsScored + goalsConceded) / matchesPlayed;

        return new Result(
            Form: formBuilder.ToString(),
            AverageGoals: Math.Round(avg, 2),
            FormattedAverageGoals: avg.ToString("0.0#", CultureInfo.InvariantCulture),
            MatchesPlayed: matchesPlayed,
            Points: points,
            GoalsScored: goalsScored,
            GoalsConceded: goalsConceded
        );
    }
}