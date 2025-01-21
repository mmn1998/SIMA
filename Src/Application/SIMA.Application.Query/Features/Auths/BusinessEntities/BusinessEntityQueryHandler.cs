using SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.BusinessEntities;

namespace SIMA.Application.Query.Features.Auths.BusinessEntities;

public class BusinessEntityQueryHandler : IQueryHandler<GetAllBusinessEntitiesQuery, Result<IEnumerable<GetBusinessEntityQueryResult>>>,
    IQueryHandler<GetBusinessEntityQuery, Result<GetBusinessEntityQueryResult>>
{
    private readonly IBusinessEntityQueryRepository _repository;

    public BusinessEntityQueryHandler(IBusinessEntityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetBusinessEntityQueryResult>>> Handle(GetAllBusinessEntitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetBusinessEntityQueryResult>> Handle(GetBusinessEntityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
}
