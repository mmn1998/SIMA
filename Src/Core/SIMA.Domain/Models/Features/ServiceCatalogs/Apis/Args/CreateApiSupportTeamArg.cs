namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class CreateApiSupportTeamArg
{
    public long Id { get;  set; }
    public long ApiId { get;  set; }
    public long StaffId { get;  set; }
    public long? DepartmentId { get;  set; }
    public long? BranchId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public long CreatedBy { get;  set; }
}
