using SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsCategories;

public interface IGoodsCategoryQueryRepository : IQueryRepository
{
    Task<GetGoodsCategoryQueryResult> GetById(GetGoodsCategoryQuery request);
    Task<Result<IEnumerable<GetGoodsCategoryQueryResult>>> GetAll(GetAllGoodsCategoriesQuery request);
}