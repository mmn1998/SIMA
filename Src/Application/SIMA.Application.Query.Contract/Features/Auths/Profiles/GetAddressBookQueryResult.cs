namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetAddressBookQueryResult
{
    public long Id { get; set; }
    public long? ProfileId { get; set; }
    public long? AddressTypeId { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
    public byte[]? CreateDate { get; set; }
    public int? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public int? ModifyBy { get; set; }
}
