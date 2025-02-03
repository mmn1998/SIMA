namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Args;

public class CreateCobitScenarioArg
{
    public long Id { get; set; }
    public long CobitScenarioCategoryId { get; set; }
    public long ScenarioId { get; set; }
    public string CobitIdentifier { get; set; }
    public long ActiveStatusId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}