using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskDegrees;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskDegrees")]
public class RiskDegreeQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskDegreeQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllRiskDegreesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskDegreeQuery { Id = id };
        return await _mediator.Send(query);
    }
}
