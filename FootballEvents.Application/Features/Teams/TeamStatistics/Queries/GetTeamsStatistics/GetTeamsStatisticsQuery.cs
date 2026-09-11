using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Features.Teams.TeamStatistics.DTOs;

namespace FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;

public sealed record GetTeamsStatisticsQuery(List<string> Teams) : IQuery<List<TeamStatisticDto>>;
