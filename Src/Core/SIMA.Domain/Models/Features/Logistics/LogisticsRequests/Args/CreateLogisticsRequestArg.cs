namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;

public class CreateLogisticsRequestArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long RequesterId { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long IssuePreorityId { get; set; }
    public long? OwnerUserId { get; set; }
    public int Weight { get; set; }
    public DateTime? DueDate { get; set; }
}