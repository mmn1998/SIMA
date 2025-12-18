using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.OwnershipTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.OwnershipTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "OwnershipTypes")]
[Authorize]
public class OwnershipTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OwnershipTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateOwnershipTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Post([FromBody] ModifyOwnershipTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Post([FromRoute] long id)
    {
        var command = new DeleteOwnershipTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
