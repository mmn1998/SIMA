namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Args;

public class CreateBrokerSecondLevelAddressBookArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}