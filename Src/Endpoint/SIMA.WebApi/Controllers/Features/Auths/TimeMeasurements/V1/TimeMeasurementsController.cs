using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.TimeMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.TimeMeasurements.V1;

[Route("basic/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "TimeMeasurements")]
public class TimeMeasurementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimeMeasurementsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.timeMeasurementPost)]
    public async Task<Result> Post([FromBody] CreateTimeMeasurementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.timeMeasurementPut)]
    public async Task<Result> Put([FromBody] ModifyTimeMeasurementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.timeMeasurementDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteTimeMeasurementCommand { Id = id };
        return await _mediator.Send(command);
    }
}