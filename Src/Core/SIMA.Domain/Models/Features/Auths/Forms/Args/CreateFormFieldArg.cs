namespace SIMA.Domain.Models.Features.Auths.Forms.Args;

public class CreateFormFieldArg
{
    public long FormId { get; set; }
    public string Name { get; set; }
    public string? Code { get; set; }
    public string? Type { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
