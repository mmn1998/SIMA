using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.PositionLevels;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.PositionLevels.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PositionLevels")]
public class PositionLevelsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionLevelsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPositionLevelQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllPositionLevelsQuery query)
    {
        return await _mediator.Send(query);
    }
}