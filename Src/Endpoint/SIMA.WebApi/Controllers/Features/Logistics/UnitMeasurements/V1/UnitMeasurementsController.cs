using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.UnitMeasurements.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UnitMeasurements")]
[Authorize]
public class UnitMeasurementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitMeasurementsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.UnitMeasurementsPost)]
    public async Task<Result> Post([FromBody] CreateUnitMeasurementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.UnitMeasurementsPut)]
    public async Task<Result> Put([FromBody] ModifyUnitMeasurementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.UnitMeasurementsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUnitMeasurementCommand { Id = id };
        return await _mediator.Send(command);
    }
}