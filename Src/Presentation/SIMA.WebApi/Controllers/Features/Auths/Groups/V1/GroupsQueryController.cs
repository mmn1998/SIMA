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
[Authorize]

public class GroupsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.GroupGetAll)]
    public async Task<Result> Get(GetAllGroupQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("GetGroupAggregate/{groupId}")]
    [SimaAuthorize(Permissions.GetGroupAggregate)]
    public async Task<Result> GetGroupAggregate([FromRoute] long groupId)
    {
        var query = new GetGroupAggregate { GroupId = groupId };
        return await _mediator.Send(query);
    }

    [HttpGet("GetGroupPermission/{FormId}/{GroupId}")]
    [SimaAuthorize(Permissions.GroupPermissionGet)]
    public async Task<Result> Get([FromRoute] GetGroupPermissionQuery query)
    {
        var request = new GetGroupPermissionQuery { GroupId = query.GroupId , FormId = query.FormId };
        return await _mediator.Send(request);
    }

    //[HttpGet("{id}")]
    //[SimaAuthorize(Permissions.GroupGet)]
    //public async Task<Result> Get([FromRoute] long id)
    //{
    //    var query = new GetGroupQuery { Id = id };
    //    return await _mediator.Send(query);
    //}

    //[HttpGet("GetGroupPermission")]
    //[SimaAuthorize(Permissions.GroupPermissionGet)]
    //public async Task<Result> Get([FromQuery] GetGroupPermissionQuery query)
    //{
    //    return await _mediator.Send(query);
    //}
    //[HttpGet("GetGroupUser")]
    //[SimaAuthorize(Permissions.GroupUserGet)]
    //public async Task<Result> Get([FromQuery] GetUserGroupQuery query)
    //{
    //    return await _mediator.Send(query);
    //}

}
