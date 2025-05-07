namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;

public class CreateBrokerAccountBookArg
{
    public long Id { get; set; }
    public long BrokerId { get; set; }
    public string? IbanNumber { get; set; }
    public int? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}