using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.HappeningPossiblities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.HappeningPossiblities.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/HappeningPossiblities")]
public class HappeningPossiblitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public HappeningPossiblitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetHappeningPossibilityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllHappeningPossiblitiesQuery query)
    {
        return await _mediator.Send(query);
    }
}