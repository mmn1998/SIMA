using SIMA.Application.Query.Contract.Features.TrustyDrafts.Resources;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.Resources;

public interface IResourceQueryRepository : IQueryRepository
{
    Task<GetResourceQueryResult> GetById(GetResourceQuery request);
    Task<Result<IEnumerable<GetResourceQueryResult>>> GetAll(GetAllResourcesQuery request);
}