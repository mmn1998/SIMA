using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Contracts;

public interface IServiceOrganizationalProjectRepository
{
    Task<ServiceOrganizationalProject> GetById(ServiceOrganizationalProjectId id);

}