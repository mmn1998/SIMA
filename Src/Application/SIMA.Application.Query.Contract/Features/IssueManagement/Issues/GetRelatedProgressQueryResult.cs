namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetRelatedProgressQueryResult
{
    public long Id { get; set; }
    public long CurrentProgressId { get; set; }
    public long ProgressId { get; set; }
    public string Name { get; set; }
    public string? HasStoredProcedure { get; set; }
    public string? ProcedureInfo { get; set; }
    public long TargetId { get; set; }
    public string TargetName { get; set; }
    public string? IsAssigneeForced { get; set; }
    public string? IsActorManager { get; set; }
    public List<StoreProcedureParams> Params { get; set; } = new();
}