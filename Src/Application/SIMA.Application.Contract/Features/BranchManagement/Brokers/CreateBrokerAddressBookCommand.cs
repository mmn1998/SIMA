namespace SIMA.Application.Contract.Features.BranchManagement.Brokers;

public class CreateBrokerAddressBookCommand
{
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public long AddressTypeId { get; set; }
}
