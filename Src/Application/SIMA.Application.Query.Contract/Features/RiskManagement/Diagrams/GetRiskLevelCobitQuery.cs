using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetRiskLevelCobitQuery : IQuery<Result<GetRiskLevelCobitQueryResult>>
{
    public long RiskLevelCobitId { get; set; }

}

public class GetRiskLevelCobitQueryResult
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? code { get; set; }
    public string? riskValueId { get; set; }
    public string? riskValueTitle { get; set; }
    public string? SeverityValueId { get; set; }
    public string? SeverityValueIdTitle { get; set; }
    public string? SeverityValueNumericValue { get; set; }
    public string? SeverityValueValuingIntervalTitle { get; set; }
    public string? CurrentOccurrenceProbabilityValueId { get; set; }
    public string? CurrentOccurrenceProbabilityValueTitle { get; set; }
    public string? CurrentOccurrenceProbabilityValueNumericValue { get; set; }
   
    public List<RiskLevelCobitChart> RiskLevelCobitChart { get; set; }
}

public class RiskLevelCobitChart
{
    public string? id { get; set; }
    public List<RiskLevelCobitChartCurrentOccurrenceProbabilityValueValueTitle> Data { get; set; }
    public string? isSelected { get; set; }
    public string? Color { get; set; }
}

public class RiskLevelCobitChartCurrentOccurrenceProbabilityValueValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
}