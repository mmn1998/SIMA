using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Channels.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Channels")]
public class ChannelsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.ChannelGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetChannelQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ChannelGetAll)]
    public async Task<Result> Get([FromBody] GetAllChannelsQuery query)
    {
        return await _mediator.Send(query);
    }
}