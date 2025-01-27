using SIMA.Application.Query.Contract.Features.RiskManagement.Severities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Severities;

public interface ISeverityQueryRepository : IQueryRepository
{
    Task<GetSeverityQueryResult> GetById(GetSeverityQuery request);
    Task<Result<IEnumerable<GetSeverityQueryResult>>> GetAll(GetAllSeveritiesQuery request);
}