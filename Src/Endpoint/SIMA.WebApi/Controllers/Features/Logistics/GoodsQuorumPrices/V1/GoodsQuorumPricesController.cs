using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsQuorumPrices.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsQuorumPrices")]
public class GoodsQuorumPricesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsQuorumPricesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateGoodsQuorumPriceCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyGoodsQuorumPriceCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGoodsQuorumPriceCommand { Id = id };
        return await _mediator.Send(command);
    }
}