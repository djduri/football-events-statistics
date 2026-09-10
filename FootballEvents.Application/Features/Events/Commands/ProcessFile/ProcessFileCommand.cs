using FootballEvents.Application.Abstractions;

namespace FootballEvents.Application.Features.Events.Commands.ProcessFile;
// Include properties to be used as input for the command
public sealed record ProcessFileCommand(Stream FileStream) : ICommand<long>;