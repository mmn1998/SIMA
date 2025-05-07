namespace SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Args;

public class CreateReferalLetterArg
{
    public long Id { get; set; }
    public string? LetterNumber { get;  set; }
    public DateTime LetterDate { get;  set; }
    public long BrokerId { get;  set; }
    public long TrustyDraftId { get;  set; }
    public long? LetterDocumentId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
