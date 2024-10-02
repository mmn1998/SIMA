using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Roles;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Roles.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Roles")]
[Authorize]

public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddRoleAggregate")]
    [SimaAuthorize(Permissions.AddRoleAggregate)]
    public async Task<Result> Post([FromBody] CreateRoleAggregate command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.RolePut)]
    public async Task<Result> Put([FromBody] UpdateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.RoleDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRoleCommand { Id = id };
        return await _mediator.Send(command);
    }


    //[HttpDelete("DeleteRolePermission")]
    //[SimaAuthorize(Permissions.RolePermissionDelete)]
    //public async Task<Result> Delete([FromBody] DeleteRolePermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPost]
    //[SimaAuthorize(Permissions.RolePost)]
    //public async Task<Result> Post([FromBody] CreateRoleCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPost("AddRolePermission")]
    //[SimaAuthorize(Permissions.RolePermissionPost)]
    //public async Task<Result> Post([FromBody] CreateRolePermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}

    //[HttpPut("EditRolePermission")]
    //[SimaAuthorize(Permissions.RolePermissionPut)]
    //public async Task<Result> Put([FromBody] UpdateRolePermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
}
