namespace SIMA.Domain.Models.Features.Auths.AddressTypes.Args;



public class ModifyAddressTypeArg
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }

}
