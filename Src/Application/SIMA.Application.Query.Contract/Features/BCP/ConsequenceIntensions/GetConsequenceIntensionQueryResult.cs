namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;

public class GetConsequenceIntensionQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float ValueNumber { get; set; }
    public string? ActiveStatus { get; set; }
}