namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class ModifyCriticalActivityArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? TechnicalSupervisorDepartmentId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}
