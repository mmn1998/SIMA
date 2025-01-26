using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.UseVulnerabilities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.UseVulnerabilities.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "EvaluationCriteria")]
public class UseVulnerabilitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public UseVulnerabilitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGet)]
    */
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetUseVulnerabilityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGetAll)]
    */
    public async Task<Result> Get([FromBody] GetAllUseVulnerabilitiesQuery query)
    {
        return await _mediator.Send(query);
    }
    
}