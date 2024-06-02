namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceUserTypes;

public class GetServiceUserTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}