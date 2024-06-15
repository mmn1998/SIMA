using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.Goods.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Goodses")]
public class GoodsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateGoodsCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyGoodsCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGoodsCommand { Id = id };
        return await _mediator.Send(command);
    }
}