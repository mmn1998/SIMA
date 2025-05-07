namespace SIMA.Domain.Models.Features.Auths.Genders.Args;

public class CreateGenderArg
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public int? ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
