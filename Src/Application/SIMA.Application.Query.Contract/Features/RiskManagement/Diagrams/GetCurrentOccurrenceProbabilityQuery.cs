using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetCurrentOccurrenceProbabilityQuery  : IQuery<Result<GetCurrentOccurrenceProbabilityQueryResult>>
{
    public long CurrentOccurrenceProbabilityId { get; set; }

}



public class GetCurrentOccurrenceProbabilityQueryResult
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? code { get; set; }
    public string? CurrentOccurrenceProbabilityValueId { get; set; }
    public string? CurrentOccurrenceProbabilityValue { get; set; }
    public string? CurrentOccurrenceProbabilityValueNumericValue { get; set; }
    public string? CurrentOccurrenceProbabilityValueValuingIntervalTitle { get; set; }
    public string? InherentOccurrenceProbabilityValueId  { get; set; }
    public string? InherentOccurrenceProbabilityValueIdTitle { get; set; }
    public string? InherentOccurrenceProbabilityValueIdNumericValue { get; set; }
    public string? FrequencyId { get; set; }
    public string? FrequencyIdTitle { get; set; }
    public string? FrequencyIdValueTitle { get; set; }
    public string? FrequencyIdNumericValue { get; set; }
    public List<CurrentOccurrenceProbabilityChart> Data { get; set; }
}

public class CurrentOccurrenceProbabilityChart
{
    public string? id { get; set; }
    public List<CurrentOccurrenceProbabilityInherentOccurrenceProbabilityValueValueTitle> Data { get; set; }
    public string? isSelected { get; set; }
    public string? Color { get; set; }
}

public class CurrentOccurrenceProbabilityInherentOccurrenceProbabilityValueValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
}