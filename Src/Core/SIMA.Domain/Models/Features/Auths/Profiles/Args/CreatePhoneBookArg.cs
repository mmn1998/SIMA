namespace SIMA.Domain.Models.Features.Auths.Profiles.Args;

public class CreatePhoneBookArg
{
    public long? ProfileId { get; set; }

    public long? PhoneTypeId { get; set; }

    public string? PhoneNumber { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? ModifiedBy { get; set; }


}
