using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskTypes;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskTypes")]
public class RiskTypeQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskTypeQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllRiskTypesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
}
