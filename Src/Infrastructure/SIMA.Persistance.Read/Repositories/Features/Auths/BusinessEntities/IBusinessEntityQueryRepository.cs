using SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.BusinessEntities;

public interface IBusinessEntityQueryRepository : IQueryRepository
{
    Task<GetBusinessEntityQueryResult> GetById(GetBusinessEntityQuery request);
    Task<Result<IEnumerable<GetBusinessEntityQueryResult>>> GetAll(GetAllBusinessEntitiesQuery request);
}