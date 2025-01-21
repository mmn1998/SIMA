namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;

public class CreateProductResponsibleArg
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long ResponsibleTypeId { get; set; }
    public long ResponsibleId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}
