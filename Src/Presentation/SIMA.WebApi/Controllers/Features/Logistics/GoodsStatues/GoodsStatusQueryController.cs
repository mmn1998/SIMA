using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsStatuses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsStatues;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsStatuses")]
[Authorize]
public class GoodsStatusQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsStatusQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllGoodsStatusQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGoodsStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
}
