namespace SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;

public class GetStrategyTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}