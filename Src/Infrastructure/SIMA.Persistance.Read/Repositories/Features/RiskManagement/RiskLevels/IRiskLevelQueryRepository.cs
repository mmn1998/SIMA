using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevels;

public interface IRiskLevelQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetRiskLevelsQueryResult>>> GetAll(GetAllRiskLevelsQuery request);
    Task<GetRiskLevelsQueryResult> GetById(long id);
}
