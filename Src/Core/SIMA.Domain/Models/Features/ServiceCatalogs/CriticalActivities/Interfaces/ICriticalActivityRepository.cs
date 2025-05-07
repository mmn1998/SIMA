using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Interfaces;

public interface ICriticalActivityRepository : IRepository<CriticalActivity>
{
    Task<CriticalActivity> GetById(CriticalActivityId id);
    Task<CriticalActivity?> GetLastCriticalActivity();
}
