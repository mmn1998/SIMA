using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.UnitMeasurements.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UnitMeasurements")]
public class UnitMeasurementsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitMeasurementsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetUnitMeasurementQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllUnitMeasurementQuery query)
    {
        return await _mediator.Send(query);
    }
}