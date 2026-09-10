using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Features.Teams.TeamStatistics.DTOs;

namespace FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatisticsQuery;
// Include properties to be used as input for the query
public sealed record GetTeamsStatisticsQuery(List<string> Teams) : IQuery<List<TeamStatisticDto>>;
