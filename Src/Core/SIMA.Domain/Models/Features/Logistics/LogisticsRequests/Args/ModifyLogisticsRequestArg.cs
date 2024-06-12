namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;

public class ModifyLogisticsRequestArg
{
    public long IssueId { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
