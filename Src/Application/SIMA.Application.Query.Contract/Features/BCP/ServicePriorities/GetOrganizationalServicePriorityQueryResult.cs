namespace SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;

public class GetOrganizationalServicePriorityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Ordering { get; set; }
    public string? ActiveStatus { get; set; }
}