using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Framework.Common.Response;

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
    public async Task<Result> Get([FromBody] GetAllRiskPossibilitiesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskPossibilityQuery { Id = id };
        return await _mediator.Send(query);
    }
}
