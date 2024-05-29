using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceAssetArg
{
    public long Id { get;  set; }
    public long ServiceId { get;  set; }
    //? asset
    public long? ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
