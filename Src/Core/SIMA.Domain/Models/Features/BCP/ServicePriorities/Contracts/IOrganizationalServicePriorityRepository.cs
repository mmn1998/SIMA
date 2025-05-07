using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ServicePriorities.Contracts;

public interface IOrganizationalServicePriorityRepository : IRepository<OrganizationalServicePriority>
{
    Task<OrganizationalServicePriority> GetById(OrganizationalServicePriorityId id);
}