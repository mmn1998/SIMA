using SIMA.Application.Query.Contract.Features.RiskManagement.RiskValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskValues;

public interface IRiskValueQueryRepository : IQueryRepository
{
    Task<GetRiskValueQueryResult> GetById(GetRiskValueQuery request);
    Task<Result<IEnumerable<GetRiskValueQueryResult>>> GetAll(GetAllRiskValuesQuery request);
}