using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetSeverityQuery : IQuery<Result<GetSeverityQueryResult>>
{
    public long SeverityChartId { get; set; }
}


public class GetSeverityQueryResult
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? code { get; set; }
    public string SeverityId { get; set; }
    public string SeverityValueId { get; set; }
    public string SeverityValueTitle { get; set; }
    public string SeverityValueNumericValue { get; set; }
    public string AffectedHistoryId { get; set; }
    public string AffectedHistoryTitle { get; set; }
    public string AffectedHistoryValueTitle { get; set; }
    public string AffectedHistoryNumericValue { get; set; }
    public string ConsequenceLevelId { get; set; }
    public string ConsequenceLevelTitle { get; set; }
    public string ConsequenceLevelNumericValue { get; set; }
    public string ConsequenceLevelValueTitle { get; set; }
    public List<severityChart> severityChart { get; set; }
}

public class severityChart
{
    public string? id { get; set; }
    public List<SeverityAffectedHistoryValueTitle> Data { get; set; }
    public string? isSelected { get; set; }
    public string? Color { get; set; }
}

public class SeverityAffectedHistoryValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
}