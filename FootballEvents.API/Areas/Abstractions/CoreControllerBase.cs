using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballEvents.API.Areas.Abstractions;

public abstract class CoreControllerBase : ControllerBase
{
    protected ActionResult<TResponse> Ok<TResponse>(TResponse value) => base.Ok(value);
    protected ActionResult<TResponse> Redirect<TResponse>(TResponse value) => base.Redirect(value!.ToString()!);
    protected ActionResult<TResponse> BadRequest<TResponse>(TResponse error) => base.BadRequest(error);
    protected ActionResult<TResponse> Created<TResponse>(string url, TResponse value) => base.Created(url, value);
    protected OkResult Ok(Unit _) => base.Ok();
    protected BadRequestResult BadRequest(Unit _) => base.BadRequest();
    protected NoContentResult NoContent(Unit _) => base.NoContent();
}
