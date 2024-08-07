using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Permissions;
using SIMA.Resources;

namespace SIMA.Application.Query.Features.Auths.Permissions;

public class PermissionQueryHandler : IQueryHandler<GetPermissionQuery, Result<GetPermissionQueryResult>>, IQueryHandler<GetAllPermissionsByDomainIdQuery, Result<IEnumerable<GetPermissionQueryResult>>>
{
    private readonly IMapper _mapper;
    private readonly IPermissionQueryRepository _repository;

    public PermissionQueryHandler(IMapper mapper, IPermissionQueryRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<GetPermissionQueryResult>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetPermissionQueryResult>>> Handle(GetAllPermissionsByDomainIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetPermissionQueryResult>>();

        if (request.DomainId is not null && request.DomainId != 0) result = await _repository.GetAll(request,  request.DomainId.Value);
        else throw new SimaResultException(CodeMessges._400Code , Messages.DomainIsNotNull);
        return result;
    }
}
