using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Application.Query.Features.Auths.Users.Mappers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Users;

namespace SIMA.Application.Query.Features.Auths.Users;
public class UserQueryHandler : IQueryHandler<LoginUserQuery, Result<LoginUserQueryResult>>,
        IQueryHandler<GetUserByProfileIdQuery, Result<GetUserByProfileIdQueryResult>>,
        IQueryHandler<GetInfoByUserIdQuery, Result<GetInfoByUserIdQueryResult>>,
        IQueryHandler<GetAllUserQuery, Result<List<GetUserQueryResult>>>,
        IQueryHandler<GetUserQuery, Result<GetUserQueryResult>>
        , IQueryHandler<GetUserPermissionQuery, Result<GetUserPermissionQueryResult>>,
        IQueryHandler<GetUserLocationQuery, Result<GetUserLocationQueryResult>>,
        IQueryHandler<GetUserDomainQuery, Result<GetUserDomainQueryResult>>, IQueryHandler<GetUserRoleQuery, Result<GetUserRoleQueryResult>>
        , IQueryHandler<GetUserAggregateQuery, Result<GetUserAggregateQueryResult>>,
         IQueryHandler<GetProfileByProfileIdQuery, Result<GetProfileByProfileIdQueryResult>>
{
    private readonly IUserQueryRepository _repository;
    private readonly IMapper _mapper;
    private readonly TokenModel _securitySettings;

    public UserQueryHandler(IUserQueryRepository userQueryRepository, IOptions<TokenModel> securitySettings, IMapper mapper)
    {
        _repository = userQueryRepository;
        _mapper = mapper;
        _securitySettings = securitySettings.Value;
    }
    public async Task<Result<LoginUserQueryResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByUsernameAndPassword(request.Username, request.Password);
        var result = UserQueryMapper.MapToToken(user, _securitySettings);
        return Result.Ok(result);
    }
    public async Task<Result<GetInfoByUserIdQueryResult>> Handle(GetInfoByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetInfoByUserId(request.UserId);
        return Result.Ok(result);
    }
    public async Task<Result<GetProfileByProfileIdQueryResult>> Handle(GetProfileByProfileIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetProfileByProfileId(request.ProfileId);
        return Result.Ok(result);
    }

    public async Task<Result<long>> Handle(GetUserByProfileIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<GetUserQueryResult>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetUserQueryResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.FindByIdQuery(request.Id);
        return Result.Ok(data);
    }

    public async Task<Result<GetUserRoleQueryResult>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserRole(request.UserRoleId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserLocationQueryResult>> Handle(GetUserLocationQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserLocation(request.UserLocationId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserDomainQueryResult>> Handle(GetUserDomainQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserDomain(request.UserDomainId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserPermissionQueryResult>> Handle(GetUserPermissionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserPermission(request.UserPermissionId);
        return Result.Ok(result);
    }

    public async Task<Result<GetUserAggregateQueryResult>> Handle(GetUserAggregateQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserAggreagate(request.UserId);
        return Result.Ok(result);
    }

    Task<Result<GetUserByProfileIdQueryResult>> IRequestHandler<GetUserByProfileIdQuery, Result<GetUserByProfileIdQueryResult>>.Handle(GetUserByProfileIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
