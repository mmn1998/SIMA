using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.StrategyTypes.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/StrategyTypes")]
public class StrategyTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public StrategyTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.strategyTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetStrategyTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.strategyTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllStrategyTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}