namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args;

public class ModifyScenarioRecoveryCriteriaArg
{
    public long Id { get; set; }
    public long ScenarioId { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
