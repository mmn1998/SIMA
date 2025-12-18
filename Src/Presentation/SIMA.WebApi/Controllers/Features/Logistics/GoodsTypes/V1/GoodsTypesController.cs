using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.GoodsTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsTypes")]
public class GoodsTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.GoodsTypesPost)]
    public async Task<Result> Post([FromBody] CreateGoodsTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.GoodsTypesPut)]
    public async Task<Result> Put([FromBody] ModifyGoodsTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.GoodsTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGoodsTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}