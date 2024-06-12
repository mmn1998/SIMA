using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ChannelTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ChannelTypes")]
public class ChannelTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetChannelTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllChannelTypeQuery query)
    {
        return await _mediator.Send(query);
    }
}