namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;

public class ProductResponsibleQuery
{
    public long? ResponsibleTypeId { get; set; }
    public string? ResponsibleTypeName { get; set; }
    public long? ResponsibleId { get; set; }
    public string? Responsible { get; set; }
}
