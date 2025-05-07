using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.Origins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.Origins.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/Origins")]
public class OriginsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OriginsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.originPost)]
    public async Task<Result> Post([FromBody] CreateOriginCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.originPut)]
    public async Task<Result> Put([FromBody] ModifyOriginCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.originDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteOriginCommand { Id = id };
        return await _mediator.Send(command);
    }
}