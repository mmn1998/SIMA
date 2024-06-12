using SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskDegrees
{
    public interface IRiskDegreeQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetRiskDegreesQueryResult>>> GetAll(GetAllRiskDegreesQuery request);
        Task<GetRiskDegreesQueryResult> GetById(long id);
    }
}
