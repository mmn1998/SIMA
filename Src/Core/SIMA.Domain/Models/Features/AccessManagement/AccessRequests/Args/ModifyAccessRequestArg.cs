namespace SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Args;

public class ModifyAccessRequestArg
{
    public long Id { get; set; }
    public string? IpSourceFrom { get; set; }
    public string? IpSourceTo { get; set; }
    public string? IpDestinationFrom { get; set; }
    public string? IpDestinationTo { get; set; }
    public string? PortDestinationFrom { get; set; }
    public string? PortDestinationTo { get; set; }
    public long NetworkProtocolId { get; set; }
    public long AccessTypeId { get; set; }
    public long IssueId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeOnly AccessDurationStartTime { get; set; }
    public TimeOnly AccessDurationEndTime { get; set; }
    public string? Description { get; set; }
    public string? HasAutoRenew { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
