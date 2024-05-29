namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class CreateApiSupportTeamArg
{
    public long ApiId { get;  set; }
    public long StaffId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
