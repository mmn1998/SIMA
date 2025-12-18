using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskLevelCobits.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/RiskLevelCobits")]
public class RiskLevelCobitsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskLevelCobitsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.RiskLevelCobitGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskLevelCobitQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.RiskLevelCobitGetAll)]
    public async Task<Result> Get([FromBody] GetAllRiskLevelCobitsQuery query)
    {
        return await _mediator.Send(query);
    }
}