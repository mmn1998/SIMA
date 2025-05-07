using SIMA.Application.Query.Contract.Features.RiskManagement.SeverityValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.SeverityValues;

public interface ISeverityValueQueryRepository : IQueryRepository
{
    Task<GetSeverityValueQueryResult> GetById(GetSeverityValueQuery request);
    Task<Result<IEnumerable<GetSeverityValueQueryResult>>> GetAll(GetAllSeverityValuesQuery request);
}