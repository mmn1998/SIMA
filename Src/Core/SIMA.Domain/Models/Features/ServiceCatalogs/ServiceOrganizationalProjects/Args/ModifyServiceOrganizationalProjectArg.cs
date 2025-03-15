namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Args;

public class ModifyServiceOrganizationalProjectArg
{
    public long Id { get; set; }
    public long? ServiceId { get; set; }
    public long? OrganizationalProjectId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}