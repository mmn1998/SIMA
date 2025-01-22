using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Roles;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Roles.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Roles")]
[Authorize]

public class RolesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.RoleGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRoleQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.RoleGetAll)]
    public async Task<Result> Get(GetAllRoleQuery request)
    {
        return await _mediator.Send(request);
    }
    [HttpGet("GetRolePermission/{RoleId}/{FormId}")]
    [SimaAuthorize(Permissions.RolePermissionGet)]
    public async Task<Result> Get([FromRoute] GetRolePermissionQuery query)
    {
        var result = new GetRolePermissionQuery { FormId = query.FormId , RoleId = query.RoleId};
        return await _mediator.Send(result);
    }
    [HttpGet("GetRoleAggregate/{roleId}")]
    [SimaAuthorize(Permissions.RoleGet)]
    public async Task<Result> GetRoleAggregate([FromRoute] long roleId)
    {
        var query = new GetRoleAggregate { RoleId = roleId };
        return await _mediator.Send(query);
    }
}
