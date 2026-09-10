using FootballEvents.API.Areas.Abstractions;
using FootballEvents.Application.Features.Teams.TeamStatistics.DTOs;
using FootballEvents.Application.Features.Teams.TeamStatistics.Queries.GetTeamsStatistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FootballEvents.API.Areas.Cms;

public class StatisticsController : CmsController
{
    private readonly ISender _sender;

    public StatisticsController(ISender sender) =>
        _sender = sender;

    [AllowAnonymous]
    [HttpPost("GetTeamStatistics")]
    [SwaggerOperation(OperationId = "GetTeamsStatistics")]
    public async Task<ActionResult<List<TeamStatisticDto>>> GetTeamsStatistics([FromBody] GetTeamsStatisticsQuery query) =>
        Ok(await _sender.Send(query));
}
