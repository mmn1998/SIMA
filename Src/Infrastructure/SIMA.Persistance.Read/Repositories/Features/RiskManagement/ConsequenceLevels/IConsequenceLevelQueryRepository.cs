using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ConsequenceLevels;

public interface IConsequenceLevelQueryRepository : IQueryRepository
{
    Task<GetConsequenceLevelQueryResult> GetById(GetConsequenceLevelQuery request);
    Task<Result<IEnumerable<GetConsequenceLevelQueryResult>>> GetAll(GetAllConsequenceLevelsQuery request);
}