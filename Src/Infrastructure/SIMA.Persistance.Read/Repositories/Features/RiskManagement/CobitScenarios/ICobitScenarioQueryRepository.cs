using SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CobitScenarios;

public interface ICobitScenarioQueryRepository : IQueryRepository
{
    Task<GetCobitScenarioQueryResult> GetById(GetCobitScenarioQuery request);
    Task<Result<IEnumerable<GetCobitScenarioQueryResult>>> GetAll(GetAllCobitScenariosQuery request);
}