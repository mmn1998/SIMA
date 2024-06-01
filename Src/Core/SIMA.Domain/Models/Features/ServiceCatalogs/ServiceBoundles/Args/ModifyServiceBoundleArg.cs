namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;

public class ModifyServiceBoundleArg
{
    public long ServiceCategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long Id { get; set; }
    public long? ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
