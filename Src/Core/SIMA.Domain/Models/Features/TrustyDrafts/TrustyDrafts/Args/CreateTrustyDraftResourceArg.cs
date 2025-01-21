namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;

public class CreateTrustyDraftResourceArg
{
    public long Id { get; set; }
    public long TrustyDraftId { get; set; }
    public long ResourceId { get; set; }
    public decimal AssignedAmount { get; set; }
    public DateTime AssignedDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
