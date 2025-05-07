using SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskTypes
{
    public interface IRiskTypeQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetRiskTypesQueryResult>>> GetAll(GetAllRiskTypesQuery request);
        Task<GetRiskTypesQueryResult> GetById(long id);
    }
}
