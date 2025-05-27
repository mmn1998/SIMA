using SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.OwnershipTypes;

namespace SIMA.Application.Query.Features.Auths.OwnershipTypes;

public class OwnershipTypeQueryHandler : IQueryHandler<GetAllOwnershipTypesQuery, Result<IEnumerable<GetOwnershipTypeQueryResult>>>,
    IQueryHandler<GetOwnershipTypeQuery, Result<GetOwnershipTypeQueryResult>>
{
    private readonly IOwnershipTypeQueryRepository _repository;

    public OwnershipTypeQueryHandler(IOwnershipTypeQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetOwnershipTypeQueryResult>> Handle(GetOwnershipTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOwnershipTypeQueryResult>>> Handle(GetAllOwnershipTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
