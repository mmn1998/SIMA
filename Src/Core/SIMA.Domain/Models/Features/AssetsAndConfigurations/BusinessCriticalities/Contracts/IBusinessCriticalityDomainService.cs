using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;

public interface IBusinessCriticalityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, BusinessCriticalityId? id = null);
    Task<bool> IsOrderingUnique(float? ordering, BusinessCriticalityId? id = null);
}