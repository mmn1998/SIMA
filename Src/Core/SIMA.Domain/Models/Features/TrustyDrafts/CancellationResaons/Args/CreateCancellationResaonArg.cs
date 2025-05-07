namespace SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Args;

public class CreateCancellationResaonArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}