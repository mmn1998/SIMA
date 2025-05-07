using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;

public class GetBrokerQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? BrokerTypeId { get; set; }
    public string? BrokerTypeName { get; set; }
    public long? ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ExpireDatePersian => ExpireDate.ToPersianDateTime();
    public IEnumerable<GetBrokerPhoneBookQueryResult>? BrokerPhoneBookList { get; set; }
    public IEnumerable<GetBrokerAddressBookQueryResult>? BrokerAddressBookList { get; set; }
    public IEnumerable<GetBrokerAccountBookQueryResult>? BrokerAccountBookList { get; set; }
}
public class GetBrokerPhoneBookQueryResult
{
    public string? PhoneNumber { get; set; }
    public long? PhoneTypeId { get; set; }
    public string? PhoneTypeName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBrokerAddressBookQueryResult
{
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public long? AddressTypeId { get; set; }
    public string? AddressTypeName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBrokerAccountBookQueryResult
{
    public string? IbanNumber { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
