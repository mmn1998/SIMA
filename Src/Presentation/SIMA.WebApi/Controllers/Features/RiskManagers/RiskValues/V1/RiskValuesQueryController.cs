using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/RiskValues")]
public class RiskValuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskValuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.RiskValueGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRiskValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.RiskValueGetAll)]
    public async Task<Result> Get([FromBody] GetAllRiskValuesQuery query)
    {
        return await _mediator.Send(query);
    }
}