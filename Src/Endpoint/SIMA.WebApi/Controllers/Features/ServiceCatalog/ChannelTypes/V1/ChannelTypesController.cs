using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ChannelTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ChannelTypes")]
public class ChannelTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateChannelTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyChannelTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteChannelTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}