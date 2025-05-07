namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;

public class ModifyLogisticsSupplyArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long RequesterId { get; set; }
    public long IssuePreorityId { get; set; }
    public int Weight { get; set; }
    public DateTime DueDate { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public string PayByFundCard { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
