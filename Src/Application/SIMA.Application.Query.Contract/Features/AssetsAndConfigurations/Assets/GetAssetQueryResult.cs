using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;

public class GetAssetQueryResult
{
    public long Id { get; set; }
    public string? SerialNumber { get; set; }
    public string? Title { get; set; }
    public string? ActiveStatus { get; set; }
    public string? Model { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}