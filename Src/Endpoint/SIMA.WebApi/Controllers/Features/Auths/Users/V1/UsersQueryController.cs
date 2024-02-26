using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using System.Security.Claims;

namespace SIMA.WebApi.Controllers.Features.Auths.Users.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    [Authorize]

    public class UsersQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersQueryController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet]
        [SimaAuthorize(Permissions.UserGetAll)]
        public async Task<Result> Get([FromQuery] BaseRequest request)
        {
            var query = new GetAllUserQuery { Request = request };
            var result = await _mediator.Send(query);
            return result;
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
            }
            var query = new GetInfoByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Result.Ok(result);
        }
    }
}
