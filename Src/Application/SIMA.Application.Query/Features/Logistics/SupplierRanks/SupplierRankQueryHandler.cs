using SIMA.Application.Query.Contract.Features.Logistics.SupplierRanks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.SupplierRanks;

namespace SIMA.Application.Query.Features.Logistics.SupplierRanks;

public class SupplierRankQueryHandler : IQueryHandler<GetSupplierRankQuery, Result<GetSupplierRankQueryResult>>,
    IQueryHandler<GetAllSupplierRanksQuery, Result<IEnumerable<GetSupplierRankQueryResult>>>
{
    private readonly ISupplierRankQueryRepository _repository;

    public SupplierRankQueryHandler(ISupplierRankQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetSupplierRankQueryResult>> Handle(GetSupplierRankQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetSupplierRankQueryResult>>> Handle(GetAllSupplierRanksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}