using FootballEvents.Application.Abstractions;

namespace FootballEvents.Application.Features.Teams.MatchRecords.Commands.AddMatchRecord;
// Include properties to be used as input for the command
public sealed record AddMatchRecordCommand(string HomeTeam,
                                           string AwayTeam,
                                           int HomeScore,
                                           int AwayScore) : ICommand<string>;