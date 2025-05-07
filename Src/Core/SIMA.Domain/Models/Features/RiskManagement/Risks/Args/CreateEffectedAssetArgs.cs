using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Args;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class CreateEffectedAssetArgs
{
    public long Id { get; set; }
    public long RiskId { get; set; }
    public long AssetId { get; set; }
    public decimal AV { get; set; }
    public float EF { get; set; }
    public float Sle { get; set; }
    public float Ale { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
public class ModifyEffectedAssetArgs
{
    public long Id { get; set; }
    public long RiskId { get; set; }
    public long AssetId { get; set; }
    public decimal AV { get; set; }
    public float EF { get; set; }
    public float Sle { get; set; }
    public float Ale { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public List<CreateVulnerabilityArg>? VulnerabilityList { get; set; }
}
