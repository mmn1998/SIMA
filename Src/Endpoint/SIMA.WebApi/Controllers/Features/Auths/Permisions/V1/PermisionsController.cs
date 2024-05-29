using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Permission;
using SIMA.Domain.Models.Features.Auths.Permissions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Permisions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Permisions")]
[Authorize]

//
public class PermisionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPermissionRepository _permissionRepository;

    public PermisionsController(IMediator mediator, IPermissionRepository permissionRepository)
    {
        _mediator = mediator;
        _permissionRepository = permissionRepository;
    }

    [HttpPost]
    [SimaAuthorize(Permissions.PermisionsPost)]
    public async Task<Result> Post(CreatePermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.PermisionsDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeletePermissionCommand { Id = id };
        return await _mediator.Send(command);
    }
    #region Helpers
    [HttpPost("AddAllPermissionsToAUser/{userId}")]
    [SimaAuthorize(Permissions.SuperAdmin)]
    public async Task<bool> Post([FromRoute] long userId)
    {
        return await _permissionRepository.AddAllPermissionsToAUser(userId);
    }
    [HttpPost("AddAllPermissionsFromAUserToAnotherUser/{ownUserId}/{targetUserId}")]
    [SimaAuthorize(Permissions.SuperAdmin)]
    public async Task<bool> Post([FromRoute] long ownUserId, [FromRoute] long targetUserId)
    {
        return await _permissionRepository.AddAllPermissionsFromAUserToAnotherUser(ownUserId, targetUserId);
    }

    #endregion
}
