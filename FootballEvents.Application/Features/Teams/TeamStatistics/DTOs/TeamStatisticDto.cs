namespace FootballEvents.Application.Features.Teams.TeamStatistics.DTOs;
public sealed class TeamStatisticDto
{
    public required string Name { get; set; }
    public required string Form { get; set; }
    public required double AverageGoals { get; set; }
    public required int MatchesPlayed { get; set; }
    public required int Points { get; set; }
    public required int GoalsScored { get; set; }
    public required int GoalsConceded { get; set; }
}
