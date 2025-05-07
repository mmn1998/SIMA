using SIMA.Application.Query.Contract.Features.Logistics.GoodsStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsStatues;

public interface IGoodsStatusQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetAllGoodsStatusQueryResult>>> GetAll(GetAllGoodsStatusQuery request);
    Task<GetAllGoodsStatusQueryResult> GetById(long id);
    Task<long> GetStatusByCode(string code);
}
