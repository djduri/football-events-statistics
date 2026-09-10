using FootballEvents.API.Areas.Abstractions;
using FootballEvents.Application.Features.Teams.TeamStatistics.DTOs;
using FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballEvents.API.Areas.Cms;

public class StatisticsController : CmsController
{
    private readonly ISender _sender;

    public StatisticsController(ISender sender) =>
        _sender = sender;

    [HttpPost("GetTeamStatistics")]
    public async Task<ActionResult<List<TeamStatisticDto>>> GetTeamsStatistics([FromBody] GetTeamsStatisticsQuery query) =>
        Ok(await _sender.Send(query));
}
