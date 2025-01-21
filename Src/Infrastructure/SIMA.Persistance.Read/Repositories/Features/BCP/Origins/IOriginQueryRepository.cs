using SIMA.Application.Query.Contract.Features.BCP.Origins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Origins;

public interface IOriginQueryRepository : IQueryRepository
{
    Task<GetOriginQueryResult> GetById(GetOriginQuery request);
    Task<Result<IEnumerable<GetOriginQueryResult>>> GetAll(GetAllOriginsQuery request);
}