namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTechnicalStatuses;

public class GetAssetTechnicalStatusQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}