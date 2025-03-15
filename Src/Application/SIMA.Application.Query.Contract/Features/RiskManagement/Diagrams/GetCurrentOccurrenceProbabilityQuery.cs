using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetCurrentOccurrenceProbabilityQuery  : IQuery<Result<GetCurrentOccurrenceProbabilityQueryResult>>
{
    public long RiskId { get; set; }

}



public class GetCurrentOccurrenceProbabilityQueryResult
{
    public List<CurrentOccurrenceProbabilityChart> CurrentOccurrenceProbabilityChart { get; set; }
}

public class CurrentOccurrenceProbabilityChart
{
    public string? id { get; set; }
    public List<CurrentOccurrenceProbabilityInherentOccurrenceProbabilityValueValueTitle> Data { get; set; }
}

public class CurrentOccurrenceProbabilityInherentOccurrenceProbabilityValueValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
    public int isSelected { get; set; }
    public string? Color { get; set; }
}