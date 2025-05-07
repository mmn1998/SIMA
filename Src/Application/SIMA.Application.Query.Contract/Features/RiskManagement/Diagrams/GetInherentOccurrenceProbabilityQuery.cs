using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetInherentOccurrenceProbabilityQuery : IQuery<Result<GetInherentOccurrenceProbabilityQueryResult>>
{
    public long RiskId { get; set; }

}

public class GetInherentOccurrenceProbabilityQueryResult
{
    public List<InherentOccurrenceProbabilityChart> InherentOccurrenceProbabilityChart { get; set; }
}

public class InherentOccurrenceProbabilityChart
{
    public string? id { get; set; }
    public List<InherentOccurrenceProbabilityScenarioHistoryValueTitle> Data { get; set; }
}


public class InherentOccurrenceProbabilityScenarioHistoryValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
    public int isSelected { get; set; }
    public string? Color { get; set; }
}