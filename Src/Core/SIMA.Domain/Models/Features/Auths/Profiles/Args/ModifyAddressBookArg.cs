namespace SIMA.Domain.Models.Features.Auths.Profiles.Args;

public class ModifyAddressBookArg
{
    public long Id { get; set; }

    public long? ProfileId { get; set; }

    public long? AddressTypeId { get; set; }

    public long? LocationId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
