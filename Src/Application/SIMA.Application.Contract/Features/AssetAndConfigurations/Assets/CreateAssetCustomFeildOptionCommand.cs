namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;

public class CreateAssetCustomFeildOptionCommand
{
    public long AsssetCustomFeildId { get; set; }
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
}