namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class GetBrokerSecondLevelAddressBookQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ActiveStatus { get; set; }
}