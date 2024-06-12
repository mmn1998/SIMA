using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskLevels;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskLevels")]
public class RiskLevelQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskLevelQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllRiskLevelsQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskLevelQuery { Id = id };
        return await _mediator.Send(query);
    }
}
