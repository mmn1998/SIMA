using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ServicePriorities.Contracts;

public interface IOrganizationalServicePriorityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, OrganizationalServicePriorityId? id = null);
}