namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Args;

public class ModifyCobitScenarioArg
{
    public long Id { get; set; }
    public long CobitRiskCategoryId { get; set; }
    public string Name { get; set; }
    public string? CobitIdentifier { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}
