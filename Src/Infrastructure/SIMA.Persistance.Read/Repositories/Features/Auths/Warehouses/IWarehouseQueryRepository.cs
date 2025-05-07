using SIMA.Application.Query.Contract.Features.Auths.Warehouses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Warehouses;

public interface IWarehouseQueryRepository : IQueryRepository
{
    Task<GetWarehouseQueryResult> GetById(GetWarehouseQuery request);
    Task<Result<IEnumerable<GetWarehouseQueryResult>>> GetAll(GetAllWarehousesQuery request);
}