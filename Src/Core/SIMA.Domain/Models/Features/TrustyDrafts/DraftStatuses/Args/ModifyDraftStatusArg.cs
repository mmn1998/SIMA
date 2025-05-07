namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Args;

public class ModifyDraftStatusArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
