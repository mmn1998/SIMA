namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;

public class GetAssetTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
}