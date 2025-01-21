namespace SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;

public class GetBusinessEntityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}
