using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Contracts;

public interface IServicePriorityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServicePriorityId? Id = null);
    Task<bool> IsOrderingUnique(int ordering, ServicePriorityId? Id = null);
}
