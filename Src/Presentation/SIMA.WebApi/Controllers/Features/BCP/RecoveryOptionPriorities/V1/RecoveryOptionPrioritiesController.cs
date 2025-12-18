using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.RecoveryOptionPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.RecoveryOptionPriorities.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/RecoveryOptionPriorities")]
public class RecoveryOptionPrioritiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecoveryOptionPrioritiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.recoveryOptionPriorityPost)]
    public async Task<Result> Post([FromBody] CreateRecoveryOptionPriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.recoveryOptionPriorityPut)]
    public async Task<Result> Put([FromBody] ModifyRecoveryOptionPriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.recoveryOptionPriorityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRecoveryOptionPriorityCommand { Id = id };
        return await _mediator.Send(command);
    }
}