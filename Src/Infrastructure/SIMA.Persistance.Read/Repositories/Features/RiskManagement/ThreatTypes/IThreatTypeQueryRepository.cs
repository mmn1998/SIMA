using SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ThreatTypes
{
    public interface IThreatTypeQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetThreatTypesQueryResult>>> GetAll(GetAllThreatTypesQuery request);
        Task<GetThreatTypesQueryResult> GetById(long id);
    }
}
