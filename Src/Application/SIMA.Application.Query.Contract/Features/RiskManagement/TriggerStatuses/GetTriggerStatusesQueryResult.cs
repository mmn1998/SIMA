namespace SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;

public class GetTriggerStatusesQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public string? ActiveStatus { get; set; }
}