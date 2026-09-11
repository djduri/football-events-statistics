using Microsoft.AspNetCore.Mvc;

namespace FootballEvents.API.Areas.Abstractions;

[ApiController]
[Route("api/cms/[controller]")]
[ApiExplorerSettings(GroupName = "Cms")]
public abstract class CmsController : ControllerBase
{
}
