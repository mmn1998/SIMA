using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceIntensions;

public interface IConsequenceIntensionQueryRepository : IQueryRepository
{
    Task<GetConsequenceIntensionQueryResult> GetById(GetConsequenceIntensionQuery request);
    Task<Result<IEnumerable<GetConsequenceIntensionQueryResult>>> GetAll(GetAllConsequenceIntensionsQuery request);
}