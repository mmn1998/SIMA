using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/RiskValues")]
public class RiskValuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskValuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.RiskValuePost)]
    public async Task<Result> Post([FromBody] CreateRiskValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.RiskValuePut)]
    public async Task<Result> Put([FromBody] ModifyRiskValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.RiskValueDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRiskValueCommand { Id = id };
        return await _mediator.Send(command);
    }
}