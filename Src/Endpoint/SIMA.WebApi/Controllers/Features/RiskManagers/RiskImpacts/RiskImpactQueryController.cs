using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskImpacts;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskImpacts")]
public class RiskImpactQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskImpactQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.RiskImpactGetAll)]
    public async Task<Result> Get([FromBody] GetAllRiskImpactsQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.RiskImpactGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskImpactQuery { Id = id };
        return await _mediator.Send(query);
    }
}
