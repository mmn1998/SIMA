using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.Goods.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Goods")]
public class GoodsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.GoodsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGoodsQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.GoodsGetAll)]
    public async Task<Result> Get([FromBody] GetAllGoodsesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("GetByCategoryId/{GoodsCategoryId}")]
    public async Task<Result> GetByCategoryId([FromRoute] long GoodsCategoryId)
    {
        var query = new GetAllGoodsByGoodsCategoryQuery { GoodsCategoryId = GoodsCategoryId };
        return await _mediator.Send(query);
    }
}