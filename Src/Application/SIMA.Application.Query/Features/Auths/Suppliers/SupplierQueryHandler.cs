using SIMA.Application.Query.Contract.Features.Auths.Suppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Suppliers;

namespace SIMA.Application.Query.Features.Auths.Suppliers;

public class SupplierQueryHandler : IQueryHandler<GetSupplierQuery, Result<GetSupplierQueryResult>>,
    IQueryHandler<GetAllSuppliersQuery, Result<IEnumerable<GetSupplierQueryResult>>>,
    IQueryHandler<GetAllOrderedNotInBlackListSuppliersQuery, Result<IEnumerable<GetAllOrderedNotInBlackListSuppliersQueryResult>>>,
    IQueryHandler<GetSupplierAccountByLogisticsSupplyQuery, Result<IEnumerable<GetSupplierAccountByLogisticsSupplyQueryResult>>>
{
    private readonly ISupplierQueryRepository _repository;

    public SupplierQueryHandler(ISupplierQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetSupplierQueryResult>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetSupplierQueryResult>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetAllOrderedNotInBlackListSuppliersQueryResult>>> Handle(GetAllOrderedNotInBlackListSuppliersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllOrderedNotInBlackList(request);
    }

    public async Task<Result<IEnumerable<GetSupplierAccountByLogisticsSupplyQueryResult>>> Handle(GetSupplierAccountByLogisticsSupplyQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetSupplierAccountByLogisticsSupply(request.LogisticsSupplyId);
    }
}