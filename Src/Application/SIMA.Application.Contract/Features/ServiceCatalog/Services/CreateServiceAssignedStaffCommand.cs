namespace SIMA.Application.Contract.Features.ServiceCatalog.Services;

public class CreateServiceAssignedStaffCommand
{
    public long ResponsibleTypeId { get; set; }
    public long StaffId { get; set; }
    public long DepartmentId { get; set; }
    public long BranchId { get; set; }
}