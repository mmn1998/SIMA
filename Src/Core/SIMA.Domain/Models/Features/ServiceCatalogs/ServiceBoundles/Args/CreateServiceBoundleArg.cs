namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;

public class CreateServiceBoundleArg
{
    public long ServiceCategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long Id { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}