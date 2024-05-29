namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class CreateCriticalActivityAssetArg
{
    public long Id { get; set; }
    public long CriticalActivityId { get; set; }
    //? asset
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
