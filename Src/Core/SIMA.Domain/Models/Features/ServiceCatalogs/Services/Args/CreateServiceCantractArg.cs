namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceCantractArg
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    //? contract
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
