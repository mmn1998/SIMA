namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;

public class CreateBrokerPhoneBookArg
{
    public long Id { get; set; }
    public long BrokerId { get; set; }
    public long PhoneTypeId { get; set; }
    public string? PhoneNumber { get; set; }
    public int? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
