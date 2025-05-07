using SIMA.Application.Query.Contract.Features.BCP.Consequences;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Consequences;

public interface IConsequenceQueryRepository : IQueryRepository
{
    Task<GetConsequenceQueryResult> GetById(GetConsequenceQuery request);
    Task<Result<IEnumerable<GetConsequenceQueryResult>>> GetAll(GetAllConsequencesQuery request);
}