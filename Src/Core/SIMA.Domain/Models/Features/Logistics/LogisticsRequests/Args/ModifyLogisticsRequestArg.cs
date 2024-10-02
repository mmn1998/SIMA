namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;

public class ModifyLogisticsRequestArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long RequesterId { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public long IssuePreorityId { get; set; }
    public int Weight { get; set; }
    public DateTime DueDate { get; set; }

}
