namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Args;

public class CreateServiceOrganizationalProjectArg
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public long OrganizationalProjectId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}