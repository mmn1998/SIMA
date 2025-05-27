using Newtonsoft.Json;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetRiskLevelCobitQuery : IQuery<Result<GetRiskLevelCobitQueryResult>>
{
    public long RiskId { get; set; }

}

public class GetRiskLevelCobitQueryResult
{
    public List<RiskLevelCobitChart> RiskLevelCobitChart { get; set; }
}

public class RiskLevelCobitChart
{
    public string? id { get; set; }
    public List<RiskLevelCobitChartCurrentOccurrenceProbabilityValueValueTitle> Data { get; set; }
}

public class RiskLevelCobitChartCurrentOccurrenceProbabilityValueValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
    public int isSelected { get; set; }
    public string? Color { get; set; }

    [JsonProperty("TNumericValue")]
    public double ahNumericValue { get; set; }

}