namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.RequestValors;

public class GetRequestValorQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}