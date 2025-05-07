namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;

public class CreateProductArg
{
    public long Id { get; set; }
    public long? ServiceStatusId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long? ProviderCompanyId { get; set; }
    public DateOnly? InServiceDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}
