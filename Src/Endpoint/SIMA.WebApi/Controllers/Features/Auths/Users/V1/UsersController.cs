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
        try
        {
            var result = await _mediator.Send(command);
            #region DisAllowMoreThanOneActiveSessions
            //string key = MemoryCacheKeys.Permissions + result.Data?.UserInfoLogin?.UserId.ToString();
            //string token = result.Data?.Token ?? string.Empty;
            //TimeSpan expirtionTime = TimeSpan.FromMinutes(_configuration.GetValue<byte>("TokenModel:TokenLifeTime"));
            //_redisService.Delete(key);
            //await _redisService.InsertAsync(key, token, expirtionTime);
            #endregion
            
            return result;
        }
        catch (Exception ex)
        {

            throw;
        }

    }
    [HttpGet("Logout")]
    public async Task Logout()
    {
        string username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
        _redisService.Delete(username);
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
    

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.UserDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUserCommand { Id = id };
        return await _mediator.Send(command);
    }

    
    [HttpGet("SSOToken")]
    [AllowAnonymous]
    public async Task<Result>? Get([FromQuery] string Ticket)
    {
        var query = new GetUserNameWithSSO { Tiket = Ticket };
        return await _mediator.Send(query);
    }

    [HttpPost("ChangePassword")]
    [SimaAuthorize(Permissions.ChangePassword)]
    public async Task<Result> Post([FromBody] ChangePasswordCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpPost("SendCode")]
    [AllowAnonymous]
    public async Task<Result> Post([FromBody] CheckUserCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpPost("ConfirmCode")]
    [AllowAnonymous]
    public async Task<Result> Post([FromBody] ConfirmCodeCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    //[HttpPost]
    //[SimaAuthorize(Permissions.UserPost)]
    //public async Task<Result> Post([FromBody] CreateUserCommand request)
    //{
    //    var result = await _mediator.Send(request);
    //    return result;
    //}


    //[HttpPut("EditUserRole")]
    //[SimaAuthorize(Permissions.UserRolePut)]
    //public async Task<Result> Put([FromBody] UpdateUserRoleCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPut("EditUserPermission")]
    //[SimaAuthorize(Permissions.UserPermissionPut)]
    //public async Task<Result> Put([FromBody] UpdateUserPermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPut("EditUserLocation")]
    //[SimaAuthorize(Permissions.UserLocationPut)]
    //public async Task<Result> Put([FromBody] UpdateUserLocationCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPut("EditUserDomain")]
    //[SimaAuthorize(Permissions.UserDomainPut)]
    //public async Task<Result> Put([FromBody] UpdateUserDomainCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}

    //[HttpDelete("DeleteUserDomain")]
    //[SimaAuthorize(Permissions.UserDomainDelete)]
    //public async Task<Result> Delete([FromQuery] DeleteUserDomainCommand command)
    //{
    //    return await _mediator.Send(command);
    //}
    //[HttpDelete("DeleteUserRole")]
    //[SimaAuthorize(Permissions.UserRoleDelete)]
    //public async Task<Result> Delete([FromQuery] DeleteUserRoleCommand command)
    //{
    //    return await _mediator.Send(command);
    //}
    //[HttpDelete("DeleteUserPermission")]
    //[SimaAuthorize(Permissions.UserPermissionDelete)]
    //public async Task<Result> Delete([FromQuery] DeleteUserPermissionCommand command)
    //{
    //    return await _mediator.Send(command);
    //}
    //[HttpDelete("DeleteUserLocation")]
    //[SimaAuthorize(Permissions.UserLocationDelete)]
    //public async Task<Result> Delete([FromQuery] DeleteUserLocationCommand command)
    //{
    //    return await _mediator.Send(command);
    //}

    //[HttpPost("AddUserRole")]
    //[SimaAuthorize(Permissions.UserRolePost)]
    //public async Task<Result> Post([FromBody] CreateUserRoleCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPost("AddUserPermission")]
    //[SimaAuthorize(Permissions.UserPermissionPost)]
    //public async Task<Result> Post([FromBody] CreateUserPermissionCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPost("AddUserLocation")]
    //[SimaAuthorize(Permissions.UserLocationPost)]
    //public async Task<Result> Post([FromBody] CreateUserLocationCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}
    //[HttpPost("AddUserDomain")]
    //[SimaAuthorize(Permissions.UserDomainPost)]
    //public async Task<Result> Post([FromBody] CreateUserDomainCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result;
    //}


}