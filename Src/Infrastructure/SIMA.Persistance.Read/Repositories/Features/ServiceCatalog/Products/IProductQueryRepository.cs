using SIMA.Application.Query.Contract.Features.Logistics.GoodsTypes;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Products;

public interface IProductQueryRepository : IQueryRepository
{
    Task<GetProductQueryResult> GetById(GetProductQuery request);
    Task<Result<IEnumerable<GetProductQueryResult>>> GetAll(GetAllProductQuery request);
    Task<string> GetLastCode();
}
