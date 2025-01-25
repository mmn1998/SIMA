namespace SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Args;

public class ModifyScenarioHistoryArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}