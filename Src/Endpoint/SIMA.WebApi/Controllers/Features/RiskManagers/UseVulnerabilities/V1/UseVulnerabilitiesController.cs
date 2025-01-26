using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.UseVulnerabilities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.UseVulnerabilities.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/UseVulnerabilities")]
public class UseVulnerabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UseVulnerabilitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    /*[SimaAuthorize(Permissions.EvaluationCriteriaPost)]*/
    public async Task<Result> Post([FromBody] CreateUseVulnerabilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    /*[SimaAuthorize(Permissions.EvaluationCriteriaPut)]*/
    public async Task<Result> Put([FromBody] ModifyUseVulnerabilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    /*[SimaAuthorize(Permissions.EvaluationCriteriaDelete)]*/
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUseVulnerabilityCommand { Id = id };
        return await _mediator.Send(command);
    }
    
}