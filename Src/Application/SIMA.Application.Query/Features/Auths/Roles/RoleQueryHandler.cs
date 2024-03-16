using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Roles;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Roles;

namespace SIMA.Application.Query.Features.Auths.Roles;

public class RoleQueryHandler : IQueryHandler<GetRoleQuery, Result<GetRoleQueryResult>>, IQueryHandler<GetAllRoleQuery, Result<IEnumerable<GetRoleQueryResult>>>,
    IQueryHandler<GetRolePermissionQuery, Result<GetRolePermissionQueryResult>>, IQueryHandler<GetRoleAggregate, Result<GetRoleAggregateResult>>
{
    private readonly IRoleQueryRepository _repository;

    public RoleQueryHandler(IRoleQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetRoleQueryResult>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetRoleQueryResult>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetRolePermissionQueryResult>> Handle(GetRolePermissionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetRolePermission(request.RolePermissionId);
        return Result.Ok(result);

    }

    public async Task<Result<GetRoleAggregateResult>> Handle(GetRoleAggregate request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetRoleAggegate(request.RoleId);
        return Result.Ok(result);
    }
}
