namespace SIMA.Domain.Models.Features.Auths.PhoneTypes.Args;

public class ModifyPhoneTypeArg
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
