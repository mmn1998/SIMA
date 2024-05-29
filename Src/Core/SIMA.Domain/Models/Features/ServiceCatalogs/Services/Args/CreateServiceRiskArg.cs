namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceRiskArg
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    //? risk
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
