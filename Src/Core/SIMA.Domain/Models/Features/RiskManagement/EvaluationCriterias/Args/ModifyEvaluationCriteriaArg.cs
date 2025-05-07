namespace SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Args;

public class ModifyEvaluationCriteriaArg
{
    public long Id { get; set; }
    public long RiskDegreeId { get; set; }
    public long RiskPossibilityId { get; set; }
    public long RiskImpactId { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
