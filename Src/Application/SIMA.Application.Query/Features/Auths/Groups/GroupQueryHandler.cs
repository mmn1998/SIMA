using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Groups;

namespace SIMA.Application.Query.Features.Auths.Groups;

public class GroupQueryHandler : IQueryHandler<GetGroupAggregate, Result<GetGroupAggregateResult>>, IQueryHandler<GetAllGroupQuery, Result<IEnumerable<GetGroupQueryResult>>>
    , IQueryHandler<GetGroupPermissionQuery, Result<List<GetGroupPermissionQueryResult>>>
    //,IQueryHandler<GetGroupQuery, Result<GetGroupQueryResult>>,
    // IQueryHandler<GetUserGroupQuery, Result<GetUserGroupQueryResult>>,

{
    private readonly IGroupQueryRepository _repository;

    public GroupQueryHandler(IGroupQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetGroupQueryResult>>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
    public async Task<Result<GetGroupAggregateResult>> Handle(GetGroupAggregate request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetGroupAggregate(request.GroupId);
        return Result.Ok(result);
    }
    public async Task<Result<List<GetGroupPermissionQueryResult>>> Handle(GetGroupPermissionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetGroupPermission(request.FormId , request.GroupId);
        return result;
    }

    //public async Task<Result<GetGroupQueryResult>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    //{
    //    var result = await _repository.FindById(request.Id);
    //    //var result = _mapper.Map<GetGroupQueryResult>(entity);
    //    return Result.Ok(result);
    //}
    //public async Task<Result<GetUserGroupQueryResult>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
    //{
    //    var result = await _repository.GetGroupUser(request.UserGroupId);
    //    return Result.Ok(result);
    //}
    //public async Task<Result<GetGroupPermissionQueryResult>> Handle(GetGroupPermissionQuery request, CancellationToken cancellationToken)
    //{
    //    var result = await _repository.GetGroupPermission(request.GroupPermissionId);
    //    return Result.Ok(result);
    //}

}
