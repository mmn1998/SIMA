using SIMA.Application.Query.Contract.Features.RiskManagement.CobitCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CobitCategories;

public interface ICobitCategoryQueryRepository : IQueryRepository
{
    Task<GetCobitCategoryQueryResult> GetById(GetCobitCategoryQuery request);
    Task<Result<IEnumerable<GetCobitCategoryQueryResult>>> GetAll(GetAllCobitCategoriesQuery request);
}