namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class CreateEffectedAssetCommand
{
    public long AssetId { get; set; }
    public decimal AV { get; set; }
    public float EF { get; set; }
    public float Sle { get; set; }
    public float Ale { get; set; }
    public string? Description { get; set; }
    public List<CreateVulnerabilityCommand>? VulnerabilityList { get; set; }
}
