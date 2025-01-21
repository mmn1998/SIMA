namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;

public class CreateBrokerAddressBookArg
{
    public long Id { get; set; }
    public long BrokerId { get; set; }
    public long AddressTypeId { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public int? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}