using FootballEvents.Application.Abstractions;
using System.Text.Json.Serialization;

namespace FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;

public sealed record CreateMatchRecordCommand(
	[property: JsonPropertyName("home_team")] string HomeTeam,
	[property: JsonPropertyName("away_team")] string AwayTeam,
	[property: JsonPropertyName("home_score")] int HomeScore,
	[property: JsonPropertyName("away_score")] int AwayScore
) : ICommand<string>;