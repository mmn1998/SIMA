using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.AccessTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.AccessTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "AccessTypes")]
[Authorize]
public class AccessTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccessTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateAccessTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Post([FromBody] ModifyAccessTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Post([FromRoute] long id)
    {
        var command = new DeleteAccessTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
