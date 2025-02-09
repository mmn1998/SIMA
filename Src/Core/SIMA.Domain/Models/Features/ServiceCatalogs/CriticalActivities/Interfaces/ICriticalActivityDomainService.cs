using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Interfaces;

public interface ICriticalActivityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CriticalActivityId? id = null);
    Task<string?> GetLastCode();
}
