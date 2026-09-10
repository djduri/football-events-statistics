using FluentValidation;

namespace FootballEvents.Application.Features.Events.Commands.ProcessFile;
public sealed class ProcessFileValidator : AbstractValidator<ProcessFileCommand>
{
    public ProcessFileValidator()
    {
        RuleFor(x => x.FileStream)
                    .NotNull()
                    .WithMessage("File stream cannot be null.");
    }
}
