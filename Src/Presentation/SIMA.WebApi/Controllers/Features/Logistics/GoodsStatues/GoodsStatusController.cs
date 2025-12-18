using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.GoodsStatues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsStatues;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsStatuses")]
[Authorize]
public class GoodsStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.GoodsStatusPost)]
    public async Task<Result> Post([FromBody] CreateGoodsStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.GoodsStatusPut)]
    public async Task<Result> Put([FromBody] ModifyGoodsStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.GoodsStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGoodsStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}