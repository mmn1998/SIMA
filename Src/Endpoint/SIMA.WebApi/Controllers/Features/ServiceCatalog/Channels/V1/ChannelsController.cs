using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.Channels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Channels.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Channels")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ChannelPost)]
    public async Task<Result> Post([FromBody] CreateChannelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ChannelPut)]
    public async Task<Result> Put([FromBody] ModifyChannelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ChannelDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteChannelCommand { Id = id };
        return await _mediator.Send(command);
    }
}