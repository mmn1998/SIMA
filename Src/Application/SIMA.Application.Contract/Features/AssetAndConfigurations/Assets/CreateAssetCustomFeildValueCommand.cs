namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;

public class CreateAssetCustomFeildValueCommand
{
    public long AssetCustomFeildId { get; set; }
    public string? Value { get; set; }
}
