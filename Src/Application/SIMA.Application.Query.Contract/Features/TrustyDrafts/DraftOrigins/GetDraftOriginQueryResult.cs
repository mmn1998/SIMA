namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftOrigins;

public class GetDraftOriginQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}