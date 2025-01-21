namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;

public class GetBusinessCriticalityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Ordering { get; set; }
    public string? ActiveStatus { get; set; }
}