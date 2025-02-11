namespace SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;

public class GetSolutionPeriorityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Priority { get; set; }
    public string? ActiveStatus { get; set; }
}