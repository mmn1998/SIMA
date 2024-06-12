namespace SIMA.Application.Query.Contract.Features.BCP.HappeningPossiblities;

public class GetHappeningPossibilityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Ordering { get; set; }
    public string? ActiveStatus { get; set; }
}