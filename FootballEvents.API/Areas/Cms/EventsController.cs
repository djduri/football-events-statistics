using FootballEvents.API.Areas.Abstractions;
using FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FootballEvents.API.Areas.Cms;

public class EventsController: CmsController
{
    private readonly ISender _sender;

    public EventsController(ISender sender) =>
        _sender = sender;
  
    [AllowAnonymous]
    [HttpPost("Result")]
    [SwaggerOperation(OperationId = "PostEventResult")]
    public async Task<ActionResult<string>> PostEventResult(CreateMatchRecordCommand command) =>
        Ok(await _sender.Send(command));   
}
