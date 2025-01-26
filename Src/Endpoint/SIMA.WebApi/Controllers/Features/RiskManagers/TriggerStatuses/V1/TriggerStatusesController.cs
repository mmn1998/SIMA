using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Application.Contract.Features.RiskManagers.TriggerStatuses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.TriggerStatuses.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "TriggerStatus")]
public class TriggerStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TriggerStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaPost)]
    */
    public async Task<Result> Post([FromBody] CreateTriggerStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaPut)]
    */
    public async Task<Result> Put([FromBody] ModifyTriggerStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaDelete)]
    */
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteTriggerStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}