using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Categories;

public interface ICategoryQueryRepository: IQueryRepository
{
    Task<GetCategoryQueryResult> GetById(GetCategoryQuery request);
    
    Task<Result<IEnumerable<GetCategoryQueryResult>>> GetAll(GetAllCategoryQuery request);
}