namespace SIMA.Application.Query.Contract.Features.Auths.MainAggregates;

public class GetMainAggregateQueryResult
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public long DomainId { get; set; }
    public string? Name { get; set; }
    public string? DomainName { get; set; }
    public string? ActiveStatus { get; set; }
    public string? Code { get; set; }
}
