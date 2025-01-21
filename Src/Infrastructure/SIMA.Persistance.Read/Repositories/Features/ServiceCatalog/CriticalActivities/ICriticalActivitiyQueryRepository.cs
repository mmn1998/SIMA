using Azure;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.CriticalActivities;

public interface ICriticalActivitiyQueryRepository : IQueryRepository
{
    Task<Result<GetCriticalActivityQueryResult>> GetDetail(long id, long issueId);
    Task<Result<IEnumerable<GetAllCriticalActivitiesQueryResult>>> GetAll(GetAllCriticalActivitiesQuery request);
}