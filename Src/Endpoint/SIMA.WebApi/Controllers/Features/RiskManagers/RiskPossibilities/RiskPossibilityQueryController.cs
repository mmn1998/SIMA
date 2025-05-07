using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskPossibilities;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskPossibilities")]
public class RiskPossibilityQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskPossibilityQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.RiskPossibilitiesGetAll)]
    public async Task<Result> Get([FromBody] GetAllRiskPossibilitiesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.RiskPossibilitiesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskPossibilityQuery { Id = id };
        return await _mediator.Send(query);
    }
}
