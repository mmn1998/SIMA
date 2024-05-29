using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Groups.V1;
[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Groups")]
//
[Authorize]

public class GroupsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.GroupGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGroupQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpGet]
    [SimaAuthorize(Permissions.GroupGetAll)]
    public async Task<Result> Get([FromQuery] GetAllGroupQuery request)
    {
        return await _mediator.Send(request);
    }
    [HttpGet("GetGroupPermission")]
    [SimaAuthorize(Permissions.GroupPermissionGet)]
    public async Task<Result> Get([FromQuery] GetGroupPermissionQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("GetGroupUser")]
    [SimaAuthorize(Permissions.GroupUserGet)]
    public async Task<Result> Get([FromQuery] GetUserGroupQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("GetGroupAggregate/{groupId}")]
    [SimaAuthorize(Permissions.GetGroupAggregate)]
    public async Task<Result> Get([FromRoute] int groupId)
    {
        var query = new GetGroupAggregate { GroupId = groupId };
        return await _mediator.Send(query);
    }
}
