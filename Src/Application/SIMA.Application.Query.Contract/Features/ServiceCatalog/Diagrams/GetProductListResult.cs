namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetProductListResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Scope { get; set; }
    public string Description { get; set; }

    public long ProviderCompanyId { get; set; }
    public string ProviderCompanyName { get; set; }

    public long ServiceStatusId { get; set; }
    public string InServiceDate { get; set; }

    public string ServiceStatusName { get; set; }

    public string ActiveStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class GetProductListResultWrapper
{
    public List<GetProductListResult>? Data { get; set; }
}



