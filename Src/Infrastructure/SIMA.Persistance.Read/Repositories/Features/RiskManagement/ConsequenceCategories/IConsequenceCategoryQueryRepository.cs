using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ConsequenceCategories;

public interface IConsequenceCategoryQueryRepository : IQueryRepository
{
    Task<GetConsequenceCategoryQueryResult> GetById(GetConsequenceCategoryQuery request);
    Task<Result<IEnumerable<GetConsequenceCategoryQueryResult>>> GetAll(GetAllConsequenceCategoriesQuery request);
}