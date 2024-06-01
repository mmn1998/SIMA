namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceBoundles;

public class GetServiceBoundleQueryResult
{
    public long Id { get; set; }
    public long ServiceCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}
