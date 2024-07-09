using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsQuorumPrices.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsQuorumPrices")]
[Authorize]
public class GoodsQuorumPricesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsQuorumPricesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.GoodsQuorumPricesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGoodsQuorumPriceQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.GoodsQuorumPricesGetAll)]
    public async Task<Result> Get([FromBody] GetAllGoodsQuorumPriceQuery query)
    {
        return await _mediator.Send(query);
    }
}