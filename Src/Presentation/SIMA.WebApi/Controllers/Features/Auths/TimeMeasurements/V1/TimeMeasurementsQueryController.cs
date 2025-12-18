using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.TimeMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.TimeMeasurements.V1;

[Route("basic/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "TimeMeasurements")]
public class TimeMeasurementsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimeMeasurementsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.timeMeasurementGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetTimeMeasurementQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.timeMeasurementGetAll)]
    public async Task<Result> Get([FromBody] GetAllTimeMeasurementsQuery query)
    {
        return await _mediator.Send(query);
    }
}