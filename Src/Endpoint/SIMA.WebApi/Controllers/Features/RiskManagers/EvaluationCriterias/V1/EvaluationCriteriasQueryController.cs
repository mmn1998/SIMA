using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.EvaluationCriterias.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "EvaluationCriteria")]
public class EvaluationCriteriasQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public EvaluationCriteriasQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.EvaluationCriteriaGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetEvaluationCriteriaQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.EvaluationCriteriaGetAll)]
    public async Task<Result> Get([FromBody] GetAllEvaluationCriteriasQuery query)
    {
        return await _mediator.Send(query);
    }
}
