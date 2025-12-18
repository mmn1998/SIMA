using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.RecoveryPointObjectives;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/RecoveryPointObjectives")]
public class RecoveryPointObjectivesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecoveryPointObjectivesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.recoveryPointObjectivesPost)]
    public async Task<Result> Post([FromBody] CreateRecoveryPointObjectiveCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.recoveryPointObjectivesPut)]
    public async Task<Result> Put([FromBody] ModifyRecoveryPointObjectiveCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.recoveryPointObjectivesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRecoveryPointObjectiveCommand { Id = id };
        return await _mediator.Send(command);
    }
}