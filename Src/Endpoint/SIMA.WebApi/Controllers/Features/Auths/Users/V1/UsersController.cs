using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Users;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.Cachings;
using System.Security.Claims;



namespace SIMA.WebApi.Controllers.Features.Auths.Users.V1;
[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Authorize]

public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedRedisService _redisService;

    public UsersController(IMediator mediator,
        IDistributedRedisService redisService)
    {
        _mediator = mediator;
        _redisService = redisService;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<Result> Post([FromBody] LoginUserQuery command)
    {
        var result = await _mediator.Send(command);
        //#region SetPermissionsInRedis
        //string key = MemoryCacheKeys.Permissions + result.Data.UserId.ToString();
        //string value = JsonSerializer.Serialize(result.Data.Permissions);

        //TimeSpan expirtionTime = TimeSpan.FromHours(3);
        //await _redisService.InsertAsync(key, value, expirtionTime);
        //#endregion
        return result;
    }
    [HttpGet("Logout")]
    public async Task Logout()
    {
        string? userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        string key = MemoryCacheKeys.Permissions + userId;
        _redisService.Delete(key);
    }
    [HttpPost]
    [SimaAuthorize(Permissions.UserPost)]
    public async Task<Result> Post([FromBody] CreateUserCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }


    [HttpPost("AddUserRole")]
    [SimaAuthorize(Permissions.UserRolePost)]
    public async Task<Result> Post([FromBody] CreateUserRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPost("AddUserPermission")]
    [SimaAuthorize(Permissions.UserPermissionPost)]
    public async Task<Result> Post([FromBody] CreateUserPermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPost("AddUserLocation")]
    [SimaAuthorize(Permissions.UserLocationPost)]
    public async Task<Result> Post([FromBody] CreateUserLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPost("AddUserDomain")]
    [SimaAuthorize(Permissions.UserDomainPost)]
    public async Task<Result> Post([FromBody] CreateUserDomainCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPost("AddUserAggregate")]
    [SimaAuthorize(Permissions.AddUserAggregate)]

    public async Task<Result> Post([FromBody] CreateUserAggregateCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPut]
    [SimaAuthorize(Permissions.UserPut)]
    public async Task<Result> Put([FromBody] UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPut("EditUserRole")]
    [SimaAuthorize(Permissions.UserRolePut)]
    public async Task<Result> Put([FromBody] UpdateUserRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPut("EditUserPermission")]
    [SimaAuthorize(Permissions.UserPermissionPut)]
    public async Task<Result> Put([FromBody] UpdateUserPermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPut("EditUserLocation")]
    [SimaAuthorize(Permissions.UserLocationPut)]
    public async Task<Result> Put([FromBody] UpdateUserLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    [HttpPut("EditUserDomain")]
    [SimaAuthorize(Permissions.UserDomainPut)]
    public async Task<Result> Put([FromBody] UpdateUserDomainCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.UserDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUserCommand { Id = id };
        return await _mediator.Send(command);
    }
    [HttpDelete("DeleteUserDomain")]
    [SimaAuthorize(Permissions.UserDomainDelete)]
    public async Task<Result> Delete([FromQuery] DeleteUserDomainCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("DeleteUserRole")]
    [SimaAuthorize(Permissions.UserRoleDelete)]
    public async Task<Result> Delete([FromQuery] DeleteUserRoleCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("DeleteUserPermission")]
    [SimaAuthorize(Permissions.UserPermissionDelete)]
    public async Task<Result> Delete([FromQuery] DeleteUserPermissionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("DeleteUserLocation")]
    [SimaAuthorize(Permissions.UserLocationDelete)]
    public async Task<Result> Delete([FromQuery] DeleteUserLocationCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("SSOToken")]
    [AllowAnonymous]
    public async Task<Result>? Get([FromQuery] string Ticket)
    {
        var query = new GetUserNameWithSSO { Tiket = Ticket };
        return await _mediator.Send(query);
    }

}