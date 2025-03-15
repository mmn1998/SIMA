using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Contracts;

public interface IOrganizationalProjectRepository : IRepository<OrganizationalProject>
{
    Task<OrganizationalProject> GetById(OrganizationalProjectId id);
}