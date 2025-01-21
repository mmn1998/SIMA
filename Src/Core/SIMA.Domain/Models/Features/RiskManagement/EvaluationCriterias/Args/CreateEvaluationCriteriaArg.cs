namespace SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Args;

public class CreateEvaluationCriteriaArg
{
    public long Id { get; set; }
    public long RiskDegreeId { get; set; }
    public long riskPossibilityId { get; set; }
    public long RiskImpactId { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}