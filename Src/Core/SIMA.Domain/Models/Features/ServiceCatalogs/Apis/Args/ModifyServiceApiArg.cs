namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class ModifyServiceApiArg
{
    public long ApiId { get;  set; }
    public long ServiceId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public byte[]? ModifiedAt { get;  set; }
    public long? ModifiedBy { get;  set; }
}
