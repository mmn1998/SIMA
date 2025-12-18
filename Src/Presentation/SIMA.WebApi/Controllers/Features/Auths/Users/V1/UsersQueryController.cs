using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.Cachings;
using System.Security.Claims;

namespace SIMA.WebApi.Controllers.Features.Auths.Users.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Authorize]

public class UsersQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedRedisService _redisService;

    public UsersQueryController(IMediator mediator, IDistributedRedisService redisService)
    {
        _mediator = mediator;
        _redisService = redisService;
    }
    [HttpGet]
    public async Task<Result> GetPrincipalFromExpiredToken(string token)
    {
        var query = new PrincipalFromExpiredTokenQuery { Token = token };
        var result = await _mediator.Send(query);
        return result;
    }
    
    [HttpPost("Revoke")]
    [AllowAnonymous]
    public async Task<Result> Revoke(RevokeQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("getProfileByUserId/{userId}")]
    public async Task<Result> GetProfileByUserId([FromRoute] long userId)
    {
        var query = new GetInfoByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("getProfileByProfileID/{profileId}")]
    public async Task<Result> GetProfileByProfileID([FromRoute] long profileId)
    {
        var query = new GetProfileByProfileIdQuery { ProfileId = profileId };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("getPrfoile/{profileId}")]
    public async Task<Result> GetProfile([FromRoute] long profileId)
    {
        var query = new GetUserByProfileIdQuery
        {
            ProfileId = profileId,
        };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.UserGetAll)]
    public async Task<Result> Get(GetAllUserQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.UserGetById)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetUserQuery
        {
            Id = id,
        };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("GetUserRole")]
    [SimaAuthorize(Permissions.UserRoleGet)]
    public async Task<Result>? Get([FromQuery] GetUserRoleQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("GetUserPermission/{UserId}/{FormId}")]
    [SimaAuthorize(Permissions.UserPermissionGet)]
    public async Task<Result>? Get([FromRoute] GetUserPermissionQuery query)
    {
        var request = new GetUserPermissionQuery { UserId = query.UserId , FormId =  query.FormId };
        return await _mediator.Send(request);
    }

    [HttpGet("GetUserLocation")]
    [SimaAuthorize(Permissions.UserLocationGet)]
    public async Task<Result>? Get([FromQuery] GetUserLocationQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }
    

    [HttpGet("GetUserAggregate/{userId}")]
    [SimaAuthorize(Permissions.UserGetById)]
    public async Task<Result>? GetAggregate([FromRoute] long userId)
    {
        var query = new GetUserAggregateQuery { UserId = userId };
        return await _mediator.Send(query);
    }
    
}
