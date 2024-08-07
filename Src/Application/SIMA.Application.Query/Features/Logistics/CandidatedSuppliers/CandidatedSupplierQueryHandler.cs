using SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.CandidatedSuppliers;

namespace SIMA.Application.Query.Features.Logistics.CandidatedSuppliers;

public class CandidatedSupplierQueryHandler :
    IQueryHandler<GetAllCandidatedSuppliersQuery, Result<IEnumerable<GetCandidatedSupplierQueryResult>>>,
    IQueryHandler<GetAllCandidatedSuppliersByLogesticIdQuery, Result<IEnumerable<GetCandidatedSupplierQueryResult>>>

{
    private readonly ICandidatedSupplierQueryRepository _repository;

    public CandidatedSupplierQueryHandler(ICandidatedSupplierQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> Handle(GetAllCandidatedSuppliersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> Handle(GetAllCandidatedSuppliersByLogesticIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByLogestictId(request.Id);
    }
}