namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetPhysicalStatuses;

public class GetAssetPhysicalStatusQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}