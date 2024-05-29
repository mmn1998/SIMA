namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceProviderArg
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public long CompanyId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
