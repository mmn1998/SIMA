namespace SIMA.Domain.Models.Features.Auths.Profiles.Args;

public class CreateProfileArg
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FatherName { get; set; }

    public long? GenderId { get; set; }

    public string? NationalId { get; set; }

    public DateOnly? Brithday { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
