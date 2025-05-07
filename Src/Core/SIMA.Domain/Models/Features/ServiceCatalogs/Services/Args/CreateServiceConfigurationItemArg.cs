namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceConfigurationItemArg
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public long ConfigurationItemId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
