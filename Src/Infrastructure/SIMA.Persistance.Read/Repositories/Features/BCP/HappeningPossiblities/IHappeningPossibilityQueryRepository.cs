using SIMA.Application.Query.Contract.Features.BCP.HappeningPossiblities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.HappeningPossiblities;

public interface IHappeningPossibilityQueryRepository : IQueryRepository
{
    Task<GetHappeningPossibilityQueryResult> GetById(GetHappeningPossibilityQuery request);
    Task<Result<IEnumerable<GetHappeningPossibilityQueryResult>>> GetAll(GetAllHappeningPossiblitiesQuery request);
}