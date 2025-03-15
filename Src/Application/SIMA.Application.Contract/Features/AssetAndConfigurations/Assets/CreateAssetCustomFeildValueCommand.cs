namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;

public class CreateAssetCustomFeildValueCommand
{
    public long AsssetCustomFeildId { get; set; }
    public string? Value { get; set; }
}
