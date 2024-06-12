using SIMA.Application.Query.Contract.Features.RiskManagement.ImpactScales;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ImpactScales
{
    public interface IImpactScaleQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetImpactScalesQueryResult>>> GetAll(GetAllImpactScalesQuery request);
        Task<GetImpactScalesQueryResult> GetById(long id);
    }
}
