using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.Orderings;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.Orderings.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Orderings")]
[Authorize]
public class OrderingsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderingsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("GetByLogesticSupplyId/{logisticsSupplyId}")]
    public async Task<Result> Get([FromRoute] long logisticsSupplyId)
    {
        var result = new GetAllOrderingssByLogesticSupplyIdQuery { LogisticsSupplyId = logisticsSupplyId };
        return await _mediator.Send(result);
    }

    [HttpPost("GetAllOrderingItemsByOrderingId/{orderingId}")]
    public async Task<Result> GetAllOrderingItemsssByOrderingId([FromRoute] long orderingId)
    {
        var result = new GetAllOrderingItemsssByOrderingIdQuery { OrderingId = orderingId };
        return await _mediator.Send(result);
    }
}