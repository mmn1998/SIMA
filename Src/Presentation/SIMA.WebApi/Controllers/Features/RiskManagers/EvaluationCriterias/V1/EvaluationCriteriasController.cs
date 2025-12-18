using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.EvaluationCriterias.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "EvaluationCriteria")]
public class EvaluationCriteriasController : ControllerBase
{
    private readonly IMediator _mediator;

    public EvaluationCriteriasController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.EvaluationCriteriaPost)]
    public async Task<Result> Post([FromBody] CreateEvaluationCriteriaCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.EvaluationCriteriaPut)]
    public async Task<Result> Put([FromBody] ModifyEvaluationCriteriaCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.EvaluationCriteriaDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteEvaluationCriteriaCommand { Id = id };
        return await _mediator.Send(command);
    }
}