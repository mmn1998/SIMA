using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.SeverityValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.SeverityValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/SeverityValues")]
public class SeverityValuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeverityValuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.SeverityValuePost)]
    public async Task<Result> Post([FromBody] CreateSeverityValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.SeverityValuePut)]
    public async Task<Result> Put([FromBody] ModifySeverityValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.SeverityValueDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSeverityValueCommand { Id = id };
        return await _mediator.Send(command);
    }
}