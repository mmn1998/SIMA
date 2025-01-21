using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Diagrams.V1;

[Route("riskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Diagrams")]
[Authorize]
public class RiskDiagramsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskDiagramsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("riskEvaluation/{id}")]
    [SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetrRskEvaluationQuery { RiskId = id };
        return await _mediator.Send(query);
    }
}
