using FootballEvents.API.Areas.Abstractions;
using MediatR;

namespace FootballEvents.API.Areas.Cms;

public class StatisticsContoller: CmsController
{
    private readonly ISender _sender;

    public StatisticsContoller(ISender sender) =>
        _sender = sender;
    /*
    [AllowAnonymous]
    [HttpPost("LinkVisit")]
    [SwaggerOperation(OperationId = "PostLinkVisit")]
    public async Task<ActionResult<long>> PostLinkVisit(CreateLinkVisitStatisticCommand command) =>
        Ok(await _sender.Send(command));

    [AllowAnonymous]
    [HttpPost("VcfDownload")]
    [SwaggerOperation(OperationId = "PostVcfDownload")]
    public async Task<ActionResult<long>> PostVcfDownload(CreateVcfDownloadStatisticCommand command) =>
        Ok(await _sender.Send(command));

    [HttpGet("SampledCardVisitStatistics")]
    [SwaggerOperation(OperationId = "GetSampledCardVisitStatistics")]
    public async Task<ActionResult<IEnumerable<VisitStatisticSampledDTO>>> GetSampledCardVisitStatistics([FromQuery] GetSampledCardVisitStatisticsQuery query) =>
        Ok(await _sender.Send(query));

    [AuthorizeRole(Roles.Admin)]
    [HttpGet("SampledAllCardVisitStatistics")]
    [SwaggerOperation(OperationId = "GetSampledAllCardVisitStatistics")]
    public async Task<ActionResult<IEnumerable<VisitStatisticSampledDTO>>> GetSampledAllCardVisitStatistics([FromQuery] GetSampledAllCardVisitStatisticsQuery query) =>
        Ok(await _sender.Send(query));

    [HttpGet("SampledLinkVisitStatistics")]
    [SwaggerOperation(OperationId = "GetSampledLinkVisitStatistics")]
    public async Task<ActionResult<IEnumerable<VisitStatisticSampledDTO>>> GetSampledLinkVisitStatistics([FromQuery] GetSampledLinkVisitStatisticsQuery query) =>
        Ok(await _sender.Send(query));

    [AuthorizeRole(Roles.Admin)]
    [HttpGet("SampledAllLinkVisitStatistics")]
    [SwaggerOperation(OperationId = "GetSampledAllLinkVisitStatistics")]
    public async Task<ActionResult<IEnumerable<VisitStatisticSampledDTO>>> GetSampledAllLinkVisitStatistics([FromQuery] GetSampledAllLinkVisitStatisticsQuery query) =>
        Ok(await _sender.Send(query));

    [AuthorizeRole(Roles.Admin)]
    [HttpGet("SampledUserCountStatistics")]
    [SwaggerOperation(OperationId = "GetSampledUserCountStatistics")]
    public async Task<ActionResult<IEnumerable<UserCountStatisticSampleDTO>>> GetSampledUserCountStatistics([FromQuery] GetSampledUserCountStatisticsQuery query) =>
        Ok(await _sender.Send(query));

    [HttpGet("SampledVcfDownloadStatistics")]
    [SwaggerOperation(OperationId = "GetSampledVcfDownloadStatistics")]
    public async Task<ActionResult<IEnumerable<VisitStatisticSampledDTO>>> GetSampledVcfDownloadStatistics([FromQuery] GetSampledVcfDownloadStatisticsQuery query) =>
        Ok(await _sender.Send(query));

    [HttpGet("SampledCardVisitGeoStatisticsByCity")]
    [SwaggerOperation(OperationId = "GetSampledCardVisitGeoStatisticsByCity")]
    public async Task<ActionResult<IEnumerable<CityVisitStatisticsDTO>>> GetSampledCardVisitGeoStatisticsByCity([FromQuery] GetSampledCardVisitGeoStatisticsByCityQuery query) =>
        Ok(await _sender.Send(query));

    [HttpGet("SampledCardVisitGeoStatisticsByCountry")]
    [SwaggerOperation(OperationId = "GetSampledCardVisitGeoStatisticsByCountry")]
    public async Task<ActionResult<IEnumerable<CountryVisitStatisticsDTO>>> GetSampledCardVisitGeoStatisticsByCountry([FromQuery] GetSampledCardVisitGeoStatisticsByCountryQuery query) =>
        Ok(await _sender.Send(query));*/
}
