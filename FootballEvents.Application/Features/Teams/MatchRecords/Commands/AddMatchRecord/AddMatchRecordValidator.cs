using FluentValidation;

namespace FootballEvents.Application.Features.Teams.MatchRecords.Commands.AddMatchRecord;
public sealed class AddMatchRecordValidator : AbstractValidator<AddMatchRecordCommand>
{
    public AddMatchRecordValidator()
    {
        RuleFor(x => x.HomeTeam).MaximumLength(255);
        RuleFor(x => x.AwayTeam).MaximumLength(255);
        RuleFor(x => x.HomeScore).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AwayScore).GreaterThanOrEqualTo(0);
    }
}
