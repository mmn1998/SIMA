using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Groups;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Groups.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Groups")]
[Authorize]

public class GroupsController : ControllerBase
{

    private readonly IMediator _mediator;
    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("AddGroupAggregate")]
    [SimaAuthorize(Permissions.AddGroupAggregate)]
    public async Task<Result> Post([FromBody] CreateGroupAggregate command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.GroupPut)]
    public async Task<Result> Put([FromBody] UpdateGroupCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.GroupDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGroupCommand { Id = id };
        return await _mediator.Send(command);
    }

    //[HttpPost]
    //[SimaAuthorize(Permissions.GroupPost)]
    //public async Task<Result> Post(CreateGroupCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}

    //[HttpPost("AddGroupUser")]
    //[SimaAuthorize(Permissions.UserGroupPost)]
    //public async Task<Result> Post([FromBody] CreateGroupUserCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPost("AddGroupPermission")]
    //[SimaAuthorize(Permissions.GroupPermissionPost)]
    //public async Task<Result> Post([FromBody] CreateGroupPermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}

    //[HttpPut("EditGroupUser")]
    //[SimaAuthorize(Permissions.UserGroupPut)]
    //public async Task<Result> Put([FromBody] UpdateGroupUserCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}

    //[HttpPut("EditGroupPermission")]
    //[SimaAuthorize(Permissions.GroupPermissionPut)]
    //public async Task<Result> Put([FromBody] UpdateGroupPermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}

    //[HttpDelete("DeleteGroupPermission")]
    //[SimaAuthorize(Permissions.GroupPermissionDelete)]
    //public async Task<Result> Delete([FromRoute] DeleteGroupPermissionCommand command)
    //{
    //    return await _mediator.Send(command);
    //}

    //[HttpDelete("DeleteGroupUser")]
    //[SimaAuthorize(Permissions.UserGroupDelete)]
    //public async Task<Result> Delete([FromRoute] DeleteUserGroupCommand command)
    //{
    //    return await _mediator.Send(command);
    //}
}
