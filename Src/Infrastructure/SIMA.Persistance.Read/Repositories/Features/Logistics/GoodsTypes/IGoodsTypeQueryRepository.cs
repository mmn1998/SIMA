using SIMA.Application.Query.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsTypes;

public interface IGoodsTypeQueryRepository : IQueryRepository
{
    Task<GetGoodsTypeQueryResult> GetById(GetGoodsTypeQuery request);
    Task<Result<IEnumerable<GetGoodsTypeQueryResult>>> GetAll(GetAllGoodsTypeQuery request);
}