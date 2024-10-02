using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.UserTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.UserTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UserTypes")]
public class UserTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateUserTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyUserTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUserTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}