namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class CreateCriticalActivityAssignedStaffArg
{
    public long Id { get; set; }
    public long CriticalActivityId { get; set; }
    //public long ResponsilbeTypeId { get; set; } //TODO
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}
