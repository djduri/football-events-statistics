using FootballEvents.API.Areas.Abstractions;
using FootballEvents.Application.Features.Teams.MatchRecords.Commands.CreateMatchRecord;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballEvents.API.Areas.Cms;

public class EventsController: CmsController
{
    private readonly ISender _sender;

    public EventsController(ISender sender) =>
        _sender = sender;  

    [HttpPost("Result")]
    public async Task<ActionResult<string>> PostEventResult([FromBody] CreateMatchRecordCommand command) =>
        Ok(await _sender.Send(command));   
}
