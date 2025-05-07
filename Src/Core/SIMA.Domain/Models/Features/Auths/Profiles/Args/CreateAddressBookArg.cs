namespace SIMA.Domain.Models.Features.Auths.Profiles.Args;

public class CreateAddressBookArg
{
    public long Id { get; set; }

    public long? ProfileId { get; set; }
    public long? LocationId { get; set; }


    public long? AddressTypeId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }
    public long ActiveStatusId { get; set; }

    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
