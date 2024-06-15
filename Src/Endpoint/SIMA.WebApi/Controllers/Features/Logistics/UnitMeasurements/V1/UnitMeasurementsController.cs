using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.UnitMeasurements.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UnitMeasurements")]
public class UnitMeasurementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitMeasurementsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateUnitMeasurementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyUnitMeasurementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUnitMeasurementCommand { Id = id };
        return await _mediator.Send(command);
    }
}