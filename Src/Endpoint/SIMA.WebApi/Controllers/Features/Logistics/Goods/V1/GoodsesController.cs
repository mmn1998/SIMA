using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.Goods.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Goods")]
[Authorize]
public class GoodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.GoodsPost)]
    public async Task<Result> Post([FromBody] CreateGoodsCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.GoodsPut)]
    public async Task<Result> Put([FromBody] ModifyGoodsCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.GoodsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGoodsCommand { Id = id };
        return await _mediator.Send(command);
    }
}