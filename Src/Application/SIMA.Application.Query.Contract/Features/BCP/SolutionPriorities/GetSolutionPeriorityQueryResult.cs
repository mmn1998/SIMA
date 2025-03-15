namespace SIMA.Application.Query.Contract.Features.BCP.SolutionPriorities;

public class GetSolutionPriorityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Priority { get; set; }
    public string? ActiveStatus { get; set; }
}