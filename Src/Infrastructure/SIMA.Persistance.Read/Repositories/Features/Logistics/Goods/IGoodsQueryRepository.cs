using SIMA.Application.Query.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.Goods;

public interface IGoodsQueryRepository : IQueryRepository
{
    Task<GetGoodsQueryResult> GetById(GetGoodsQuery request);
    Task<Result<IEnumerable<GetGoodsQueryResult>>> GetAll(GetAllGoodsesQuery request);
    Task<Result<IEnumerable<GetGoodsQueryResult>>> GetbyGoodsCategoryId(long goodsCategoryId);
}