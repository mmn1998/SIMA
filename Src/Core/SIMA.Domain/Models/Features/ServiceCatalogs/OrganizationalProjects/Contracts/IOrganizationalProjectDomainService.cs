using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Contracts;

public interface IOrganizationalProjectDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, OrganizationalProjectId? id = null);
}