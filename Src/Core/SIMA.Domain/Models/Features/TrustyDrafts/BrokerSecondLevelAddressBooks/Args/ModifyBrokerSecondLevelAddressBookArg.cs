namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Args;

public class ModifyBrokerSecondLevelAddressBookArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}