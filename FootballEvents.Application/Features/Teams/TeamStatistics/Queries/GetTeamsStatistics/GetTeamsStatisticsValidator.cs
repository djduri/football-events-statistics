using FluentValidation;

namespace FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;
public sealed class GetTeamsStatisticsValidator : AbstractValidator<GetTeamsStatisticsQuery>
{
    public GetTeamsStatisticsValidator()
    {
        RuleFor(x => x.Teams)
            .NotEmpty()
            .WithMessage("Teams list cannot be empty.");

        RuleFor(x => x.Teams)
            .Must(teams => teams == null || teams.Distinct(StringComparer.OrdinalIgnoreCase).Count() == teams.Count)
            .WithMessage("Teams list cannot contain duplicate entries.");
    }
}
