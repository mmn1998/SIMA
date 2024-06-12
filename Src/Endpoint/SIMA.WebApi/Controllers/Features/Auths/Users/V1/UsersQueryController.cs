using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

public class UsersQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedRedisService _redisService;

    public UsersQueryController(IMediator mediator, IDistributedRedisService redisService)
    {
        _mediator = mediator;
        _redisService = redisService;
    }

    [HttpGet("getProfileByUserId/{userId}")]
    public async Task<Result> GetProfileByUserId([FromRoute] long userId)
    {
        var query = new GetInfoByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("getProfileByProfileID/{profileId}")]
    public async Task<Result<GetProfileByProfileIdQueryResult>> GetProfileByProfileID([FromRoute] long profileId)
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

    [HttpGet("GetUserPermission")]
    [SimaAuthorize(Permissions.UserPermissionGet)]
    public async Task<Result>? Get([FromQuery] GetUserPermissionQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("GetUserLocation")]
    [SimaAuthorize(Permissions.UserLocationGet)]
    public async Task<Result>? Get([FromQuery] GetUserLocationQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("GetUserDomain")]
    [SimaAuthorize(Permissions.UserDomainGet)]
    public async Task<Result>? Get([FromQuery] GetUserDomainQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("GetUserAggregate/{userId}")]
    [SimaAuthorize(Permissions.GetUserAggregate)]
    public async Task<Result>? GetAggregate([FromRoute] long userId)
    {
        var query = new GetUserAggregateQuery { UserId = userId };
        return await _mediator.Send(query);
    }
    [HttpGet("is-authenticated")]
    [AllowAnonymous]
    public async Task<Result> IsAuthorize()
    {
        /// TODO This should be modified
        long userId = 0;
        bool isAuthorized = false;
        if (HttpContext.User.Identity?.IsAuthenticated ?? false)
        {
            isAuthorized = true;
            userId = Convert.ToInt64(HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            string key = MemoryCacheKeys.Permissions + userId.ToString();
            string currentToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "").Replace("bearer ", "");
            // Perminant token for test
            string permenantToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiIwMTIzNDU2Nzg5IiwiQ29tcGFueUlkIjoiMyIsInJvbGUiOiJbMiwxMiwxNV0iLCJHcm91cHMiOiJbMTQsMTcwNTQxNDgyMF0iLCJQZXJtaXNzaW9ucyI6Ilx1MDAwMVxuXHUwMDBCXGZcclx1MDAwRVx1MDAwRlx1MDAxMFx1MDAxMVx1MDAxMlx1MDAxM1x1MDAwMlx1MDAxNMOIw4nDisOLw4zDjcOOw4_DkMORXHUwMDE1w5LDk8OUw5XDlsOXw5jDmcOaw5tcdTAwMTbDnMOdw57goq_Dn8Ogw6HDosOjw6TDpVx1MDAxN8Omw6fDqMOpw6rDq8Osw63DrsOvXHUwMDE4w7DDscOyw7PDtMO1w7hcdTAwMTnDvFx1MDAxQVx1MDAxQlx1MDAxQ1x1MDAxRFx1MDAwM1x1MDAxRVx1MDAxRiDnv78hXCIjJCUmJ1x1MDAwNCjGkMaRxpLGk8aUxpXGlsaXxpgpKsalxqbGp8aoxqnGqsarxqzGrSvGrsavxrDGscayxrPGtMa1xrbGtyzGuMa5xrrGu8a8xr3Gvsa_LS7HjceOx4_HkMeRL8eXMDFcdTAwMDUyx7QzNDU2Nzg5OjtcdTAwMDY8yZnJmsmbyZzJnT3Jo8mkyaXJpsmnPsmtya7Jr8mwybE_ybfJuMm5ybrJu0DKgcqCyoPKhMqFQcqPypDKkcqSypNCypTKlcqWypfKmMqZyprKm8qcyp3KnsqfyqDKocqiyqPKpMqlyqbKp1x1MDAwN8q8yr3Kvsq_y4DLgVxiXHQiLCJuYmYiOjE3MTM5NDQzNDUsImV4cCI6MTcxNjUzNjM0NSwiaWF0IjoxNzEzOTQ0MzQ1LCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.PRMd1bZhLK_Ed6BGT2FrqEvgzgPReE3Ue6XRZ-Lnx2A";

            if (!string.Equals(currentToken, permenantToken, StringComparison.InvariantCultureIgnoreCase))
            {
                string? redisToken = await _redisService.GetAsync(key);
                if (!string.IsNullOrEmpty(redisToken) && !string.Equals(redisToken, currentToken, StringComparison.InvariantCultureIgnoreCase))
                {
                    isAuthorized = false;
                    return Result.AccessDeny();
                }
            }
        }
        var query = new GetInfoByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Result.Ok(result);
    }
}
