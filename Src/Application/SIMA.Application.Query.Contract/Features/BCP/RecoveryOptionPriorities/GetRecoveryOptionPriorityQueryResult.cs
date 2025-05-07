namespace SIMA.Application.Query.Contract.Features.BCP.RecoveryOptionPriorities;

public class GetRecoveryOptionPriorityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Ordering { get; set; }
    public string? ActiveStatus { get; set; }
}