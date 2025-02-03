using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetInherentOccurrenceProbabilityQuery : IQuery<Result<GetInherentOccurrenceProbabilityQueryResult>>
{
    public long InherentOccurrenceProbabilityId { get; set; }

}

public class GetInherentOccurrenceProbabilityQueryResult
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? code { get; set; }
    public string? InherentOccurrenceProbabilityId { get; set; }
    public string? InherentOccurrenceProbabilityValueId { get; set; }
    public string? InherentOccurrenceProbabilityValueTitle { get; set; }
    public string? InherentOccurrenceProbabilityValueNumericValue { get; set; }
    public string? MatrixAValueId { get; set; }
    public string? MatrixAValueTitle { get; set; }
    public string? MatrixAValueValueTitle { get; set; }
    public string? MatrixAValueNumericValue { get; set; }
    public string? ScenarioHistoryId { get; set; }
    public string? ScenarioHistoryTitle { get; set; }
    public string? ScenarioHistoryValueTitle { get; set; }
    public string? ScenarioHistoryNumericValue { get; set; }
    public List<InherentOccurrenceProbabilityChart> Data { get; set; }
}

public class InherentOccurrenceProbabilityChart
{
    public string? id { get; set; }
    public List<InherentOccurrenceProbabilityScenarioHistoryValueTitle> Data { get; set; }
    public string? isSelected { get; set; }
    public string? Color { get; set; }
}


public class InherentOccurrenceProbabilityScenarioHistoryValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
}