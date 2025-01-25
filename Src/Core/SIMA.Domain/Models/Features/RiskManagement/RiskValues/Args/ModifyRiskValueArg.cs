namespace SIMA.Domain.Models.Features.RiskManagement.RiskValues.Args;

public class ModifyRiskValueArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Color { get; set; }
    public string? Condition { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}