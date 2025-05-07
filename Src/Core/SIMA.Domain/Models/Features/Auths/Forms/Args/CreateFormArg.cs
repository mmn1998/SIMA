namespace SIMA.Domain.Models.Features.Auths.Forms.Args;

public class CreateFormArg
{
    public long DomainId { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? Code { get; set; }
    public string? IsSystemForm { get; set; }
    public string? JsonContent { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}

