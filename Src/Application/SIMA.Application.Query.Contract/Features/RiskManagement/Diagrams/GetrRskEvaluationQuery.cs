using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetrRskEvaluationQuery : IQuery<Result<GetrRskEvaluationQueryResult>>
{
    public long RiskId { get; set; }
}
public class GetrRskEvaluationQueryResult
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? code { get; set; }
    public string? Description { get; set; }
    public long? riskImpactAvarage { get; set; }
    public long? riskPossibilityAvarage { get; set; }
    public long? riskTypeId { get; set; }
    public string? riskTypeName { get; set; }
    public long? riskLevelId { get; set; }
    public string? riskLevelName { get; set; }
    public long? riskDegreeId { get; set; }
    public string? riskDegreeName { get; set; }
    public List<FmeaHeatData>? fmeaHeatData { get; set; }
    public List<RiskLevelHeatData>? riskLevelHeatData { get; set; }
}