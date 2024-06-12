using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.ImpactScales;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ImpactScales;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ImpactScales")]
public class ImpactScaleQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImpactScaleQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllImpactScalesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetImpactScaleQuery { Id = id };
        return await _mediator.Send(query);
    }
}
