using FootballEvents.API.Areas.Abstractions;
using FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
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

    //[AllowAnonymous]
    //[HttpPost("Result")]
    //[SwaggerOperation(OperationId = "GetTeamsStatistics")]
    //public async Task<ActionResult<string>> GetTeamsStatistics(CreateMatchRecordCommand query) =>
    //    Ok(await _sender.Send(query));
}
