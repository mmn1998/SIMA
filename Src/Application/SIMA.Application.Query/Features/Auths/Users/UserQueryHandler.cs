using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Application.Query.Features.Auths.Users.Mappers;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Common.Services;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Users;
using SIMA.Resources;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIMA.Application.Query.Features.Auths.Users;
public class UserQueryHandler(IUserQueryRepository userQueryRepository, IOptions<TokenModel> securitySettings, IMapper mapper, IUnitOfWork unitOfWork, IDistributedRedisService redisService, ITokenService tokenService) : IQueryHandler<LoginUserQuery, Result<LoginUserQueryResult>>,

        IQueryHandler<GetInfoByUserIdQuery, Result<GetInfoByUserIdQueryResult>>,
        IQueryHandler<GetAllUserQuery, Result<IEnumerable<GetUserQueryResult>>>,
        IQueryHandler<GetUserQuery, Result<GetUserQueryResult>>
        , IQueryHandler<GetUserPermissionQuery, Result<GetUserPermissionQueryResult>>,
        IQueryHandler<GetUserLocationQuery, Result<GetUserLocationQueryResult>>,
        IQueryHandler<GetUserDomainQuery, Result<GetUserDomainQueryResult>>, IQueryHandler<GetUserRoleQuery, Result<GetUserRoleQueryResult>>
        , IQueryHandler<GetUserAggregateQuery, Result<GetUserAggregateQueryResult>>,
         IQueryHandler<GetProfileByProfileIdQuery, Result<GetProfileByProfileIdQueryResult>>
         , IQueryHandler<RevokeQuery, Result<RevokeQueryResult>>
{
    private readonly IDistributedRedisService _redisService = redisService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<LoginUserQueryResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userQueryRepository.GetByUsernameAndPassword(request.Username, request.Password);

        await unitOfWork.SaveChangesAsync();

        if (user.UserInfoLogin.AccessFailedCount > 0)
            throw new SimaResultException(CodeMessges._400Code, Messages.InvalidUsernameOrPasswordError);


        if (user.UserInfoLogin.IsLocked == "1") throw new SimaResultException(CodeMessges._400Code, Messages.UserIsLocked);
        var result = UserQueryMapper.MapToToken(user, _tokenService);
        //insert refreshToken in Redis
        await _redisService.InsertAsync(user.UserInfoLogin.Username, result.RefreshToken, TimeSpan.FromHours(securitySettings.Value.RefreshTokenLifeTime));
        return Result.Ok(result);
    }
    public async Task<Result<GetInfoByUserIdQueryResult>> Handle(GetInfoByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetInfoByUserId(request.UserId);
        return Result.Ok(result);
    }
    public async Task<Result<GetProfileByProfileIdQueryResult>> Handle(GetProfileByProfileIdQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetProfileByProfileId(request.ProfileId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetUserQueryResult>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        return await userQueryRepository.GetAll(request);
    }

    public async Task<Result<GetUserQueryResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var data = await userQueryRepository.FindByIdQuery(request.Id);
        return Result.Ok(data);
    }

    public async Task<Result<GetUserRoleQueryResult>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetUserRole(request.UserRoleId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserLocationQueryResult>> Handle(GetUserLocationQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetUserLocation(request.UserLocationId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserDomainQueryResult>> Handle(GetUserDomainQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetUserDomain(request.UserDomainId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserPermissionQueryResult>> Handle(GetUserPermissionQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetUserPermission(request.UserPermissionId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserAggregateQueryResult>> Handle(GetUserAggregateQuery request, CancellationToken cancellationToken)
    {
        var result = await userQueryRepository.GetUserAggreagate(request.UserId);
        return Result.Ok(result);
    }

    public async Task<Result<RevokeQueryResult>> Handle(RevokeQuery request, CancellationToken cancellationToken)
    {
        var response = new RevokeQueryResult();
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);
        string userName = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        var realRefreshToken = await _redisService.GetAsync(userName);
        if (!string.IsNullOrEmpty(realRefreshToken) && string.Equals(realRefreshToken, request.RefreshToken, StringComparison.InvariantCultureIgnoreCase))
        {
            var accessToken = _tokenService.GenerateNewAccessTokenFromExpiredToken(principal);
            response.AccessToken = accessToken;
        }
        else throw SimaResultException.UnAuthorize;
        return response;
    }    
}
