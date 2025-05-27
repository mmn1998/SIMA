using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.BusinessContinuityStrategies.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BusinessContinuityStrategies")]
public class BusinessContinuityStrategiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessContinuityStrategiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    /*
    [SimaAuthorize(Permissions.businessContinuityStratgyPost)]
    */
    public async Task<Result> Post([FromBody] CreateBusinessContinuityStrategyCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.businessContinuityStratgyPut)]
    public async Task<Result> Put([FromBody] ModifyBusinessContinuityStrategyCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.businessContinuityStratgyDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBusinessContinuityStrategyCommand { Id = id };
        return await _mediator.Send(command);
    }
}