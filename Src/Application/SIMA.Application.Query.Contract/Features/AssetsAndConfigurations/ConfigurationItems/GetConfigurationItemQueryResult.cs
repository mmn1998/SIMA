namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;

public class GetConfigurationItemQueryResult
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public string? ActiveStatus { get; set; }
}
