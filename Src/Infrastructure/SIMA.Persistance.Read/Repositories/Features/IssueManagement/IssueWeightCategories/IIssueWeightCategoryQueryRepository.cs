using SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;

public interface IIssueWeightCategoryQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetIssueWeightCategoryQueryResult>>> GetAll(GetAllIssueWeightCategoriesQuery request);
    Task<GetIssueWeightCategoryQueryResult> GetById(long id);
    Task<long> GetIdByWeight(int weight);
    Task<string> GetByWeight(int weight);

}
