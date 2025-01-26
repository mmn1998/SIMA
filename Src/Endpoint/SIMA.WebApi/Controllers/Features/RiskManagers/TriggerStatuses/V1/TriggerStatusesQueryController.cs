using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.TriggerStatuses.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/TriggerStatus")]
public class TriggerStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public TriggerStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGet)]
    */
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetTriggerStatusesQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGetAll)]
    */
    public async Task<Result> Get([FromBody] GetAllTriggerStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
    
}