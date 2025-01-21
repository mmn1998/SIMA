namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemStatuses;

public class GetConfigurationItemStatusQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}