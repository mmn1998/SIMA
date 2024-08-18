namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;

public class CreateProductResponsibleArg
{
    public long Id { get; set; }
    public long ResposibleTypeId { get; set; }
    public long ResponsilbeId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}
