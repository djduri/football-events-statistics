using FootballEvents.API.Areas.Abstractions;
using FootballEvents.Application.Features.Events.Commands.ProcessFile;
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

    [HttpPost("ProcessFile")]
    public async Task<ActionResult<long>> ProcessFile(IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return new BadRequestObjectResult("No file was uploaded or the file is empty.");

        using var stream = file.OpenReadStream();

        var command = new ProcessFileCommand(stream);
        var processedLinesCount = await _sender.Send(command, cancellationToken);

        return Ok(processedLinesCount);
    }
}
