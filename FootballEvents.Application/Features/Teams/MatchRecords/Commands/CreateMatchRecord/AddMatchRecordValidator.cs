using FluentValidation;
using FootballEvents.Domain.Extensions;

namespace FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
public sealed class CreateMatchRecordValidator : AbstractValidator<CreateMatchRecordCommand>
{
    public CreateMatchRecordValidator()
    {
        RuleFor(x => x.HomeTeam).NotEmpty()
                                .MaximumLength(255);

        RuleFor(x => x.AwayTeam).NotEmpty()
                                .MaximumLength(255);

        RuleFor(x => x)
            .Must(x => x.HomeTeam.ToNormalizedKey() != x.AwayTeam.ToNormalizedKey())
            .WithMessage("Home team and away team cannot be the same.")
            .WithName("AwayTeam"); 

        RuleFor(x => x.HomeScore).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AwayScore).GreaterThanOrEqualTo(0);
    }
}
