namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceAssignedStaffArg
{
    public long Id { get;  set; }
    public long ServiceId { get;  set; }
    public long StaffId { get;  set; }
    public long ResponsibleTypeId { get;  set; }
    public long? BranchId { get; set; }
    public long? DepartmentId { get; set; }
    public long ActiveStatusId { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public long CreatedBy { get;  set; }

}
