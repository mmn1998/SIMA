using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.Goods.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Goodses")]
public class GoodsesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGoodsQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllGoodsesQuery query)
    {
        return await _mediator.Send(query);
    }
}