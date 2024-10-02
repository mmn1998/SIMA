using SIMA.Application.Query.Contract.Features.Auths.Suppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Suppliers;

public interface ISupplierQueryRepository : IQueryRepository
{
    Task<GetSupplierQueryResult> GetById(GetSupplierQuery request);
    Task<Result<IEnumerable<GetSupplierQueryResult>>> GetAll(GetAllSuppliersQuery request);
    Task<Result<IEnumerable<GetAllOrderedNotInBlackListSuppliersQueryResult>>> GetAllOrderedNotInBlackList(GetAllOrderedNotInBlackListSuppliersQuery request);
}