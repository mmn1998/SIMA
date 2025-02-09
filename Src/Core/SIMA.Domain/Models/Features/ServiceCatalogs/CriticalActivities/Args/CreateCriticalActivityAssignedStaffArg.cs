namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class CreateCriticalActivityAssignedStaffArg
{
    public long Id { get; set; }
    public long CriticalActivityId { get; set; }
    public long ResponsibleTypeId { get; set; }
    public long StaffId { get; set; }
    public long? BranchId { get; set; }
    public long? DepartmentId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}
