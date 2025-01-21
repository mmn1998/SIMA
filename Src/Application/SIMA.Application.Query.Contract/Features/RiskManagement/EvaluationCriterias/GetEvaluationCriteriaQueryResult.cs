namespace SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;

public class GetEvaluationCriteriaQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long RiskDegreeId { get; set; }
    public string? RiskDegreeName { get; set; }
    public long RiskPossibilityId { get; set; }
    public string? RiskPossibilityName { get; set; }
    public long RiskImpactId { get; set; }
    public string? RiskImpactName { get; set; }
    public string? ActiveStatus { get; set; }
}
