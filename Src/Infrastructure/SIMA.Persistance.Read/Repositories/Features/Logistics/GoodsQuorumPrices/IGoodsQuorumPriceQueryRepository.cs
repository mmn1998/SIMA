using SIMA.Application.Query.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsQuorumPrices;

public interface IGoodsQuorumPriceQueryRepository : IQueryRepository
{
    Task<GetGoodsQuorumPriceQueryResult> GetById(GetGoodsQuorumPriceQuery request);
    Task<Result<IEnumerable<GetGoodsQuorumPriceQueryResult>>> GetAll(GetAllGoodsQuorumPriceQuery request);
}