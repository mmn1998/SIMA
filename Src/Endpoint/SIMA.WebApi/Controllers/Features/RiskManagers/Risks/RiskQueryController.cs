using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Risks;

[Route("riskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Risks")]
public class RisksQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public RisksQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.RisksGetAll)]
    public async Task<Result> Post([FromBody] GetAllRisksQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.RisksGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskQuery { Id = id };
        return await _mediator.Send(query);
    }
}
