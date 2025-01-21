using SIMA.Application.Query.Contract.Features.BCP.Origins;
using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Scenarios
{
    public interface IScenarioQueryRepository : IQueryRepository
    {
        Task<GetScenarioQueryResult> GetById(GetScenarioQuery request);
        Task<Result<IEnumerable<GetScenarioQueryResult>>> GetAll(GetAllScenariosQuery request);
    }
}
