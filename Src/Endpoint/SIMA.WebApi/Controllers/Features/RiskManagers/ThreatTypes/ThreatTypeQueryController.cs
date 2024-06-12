using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ThreatTypes;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ThreatTypes")]
public class ThreatTypeQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ThreatTypeQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllThreatTypesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetThreatTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
}
