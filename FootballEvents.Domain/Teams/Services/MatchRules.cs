namespace FootballEvents.Domain.Teams.Services;

/// <summary>
/// Defines domain rules for football match scoring and result outcomes.
/// </summary>
public static class MatchRules
{
    public const int WinPoints = 3;
    public const int DrawPoints = 1;
    public const int LossPoints = 0;

    public const char WinChar = 'W';
    public const char DrawChar = 'D';
    public const char LossChar = 'L';

    /// <summary>
    /// Calculates the number of points earned based on goals scored and conceded.
    /// </summary>
    public static int CalculatePoints(int scored, int conceded)
    {
        if (scored > conceded) return WinPoints;
        if (scored == conceded) return DrawPoints;
        return LossPoints;
    }

    /// <summary>
    /// Determines the single-character form outcome representation ('W', 'D', 'L').
    /// </summary>
    public static char CalculateOutcomeChar(int scored, int conceded)
    {
        if (scored > conceded) return WinChar;
        if (scored == conceded) return DrawChar;
        return LossChar;
    }
}