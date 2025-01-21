namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReconsilationTypes;

public class GetReconsilationTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}