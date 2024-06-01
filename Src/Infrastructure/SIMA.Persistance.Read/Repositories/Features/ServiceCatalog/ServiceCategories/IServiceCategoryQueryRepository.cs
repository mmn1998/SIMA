using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCategories;

public interface IServiceCategoryQueryRepository : IQueryRepository
{
    Task<GetServiceCategoryQueryResult> GetById(GetServiceCategoryQuery request);
    Task<Result<IEnumerable<GetServiceCategoryQueryResult>>> GetAll(GetAllServiceCategoriesQuery request);
}