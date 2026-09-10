namespace FootballEvents.Domain.Teams.Services;

/// <summary>
/// Defines domain rules for football match scoring and result outcomes.
/// </summary>
public static class MatchRules
{
    public const int WinPoints = 3;
    public const int DrawPoints = 1;
    public const int LossPoints = 0;

    /// <summary>
    /// Calculates the number of points earned based on goals scored and conceded.
    /// </summary>
    /// <param name="scored">Goals scored by the team.</param>
    /// <param name="conceded">Goals conceded by the team.</param>
    /// <returns>Points earned (3 for win, 1 for draw, 0 for loss).</returns>
    public static int CalculatePoints(int scored, int conceded)
    {
        if (scored > conceded) return WinPoints;
        if (scored == conceded) return DrawPoints;
        return LossPoints;
    }

    /// <summary>
    /// Determines the single-character form outcome representation ('W', 'D', 'L').
    /// </summary>
    /// <param name="scored">Goals scored by the team.</param>
    /// <param name="conceded">Goals conceded by the team.</param>
    /// <returns>Character representing the match outcome ('W' for win, 'D' for draw, 'L' for loss).</returns>
    public static char CalculateOutcomeChar(int scored, int conceded)
    {
        if (scored > conceded) return 'W';
        if (scored == conceded) return 'D';
        return 'L';
    }
}