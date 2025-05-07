namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;

public class GetServiceCategoryQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
}