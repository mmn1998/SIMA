using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.UnitMeasurements.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UnitMeasurements")]
[Authorize]
public class UnitMeasurementsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitMeasurementsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.UnitMeasurementsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetUnitMeasurementQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.UnitMeasurementsGetAll)]
    public async Task<Result> Get([FromBody] GetAllUnitMeasurementQuery query)
    {
        return await _mediator.Send(query);
    }
}