namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class ModifyApiSupportTeamArg
{
    public long ApiId { get;  set; }
    public long StaffId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public byte[]? ModifiedAt { get;  set; }
    public long? ModifiedBy { get;  set; }
}
