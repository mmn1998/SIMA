using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.BusinessCriticalities;

public interface IBusinessCriticalityQueryRepository : IQueryRepository
{
    Task<GetBusinessCriticalityQueryResult> GetById(GetBusinessCriticalityQuery request);
    Task<Result<IEnumerable<GetBusinessCriticalityQueryResult>>> GetAll(GetAllBusinessCriticalitiesQuery request);
}