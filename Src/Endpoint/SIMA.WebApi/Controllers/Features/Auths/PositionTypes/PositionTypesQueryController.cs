using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.PositionTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.PositionTypes;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PositionTypes")]
[Authorize]
public class PositionTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPositionTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllPositionTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}