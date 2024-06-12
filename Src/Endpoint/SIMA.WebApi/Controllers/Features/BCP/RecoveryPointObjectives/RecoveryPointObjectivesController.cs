using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.RecoveryPointObjectives;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RecoveryPointObjectives")]
public class RecoveryPointObjectivesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecoveryPointObjectivesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateRecoveryPointObjectiveCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyRecoveryPointObjectiveCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRecoveryPointObjectiveCommand { Id = id };
        return await _mediator.Send(command);
    }
}