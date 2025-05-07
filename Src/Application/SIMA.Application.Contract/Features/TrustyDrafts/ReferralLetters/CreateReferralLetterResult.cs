namespace SIMA.Application.Contract.Features.TrustyDrafts.ReferralLetters
{
    public class CreateReferralLetterResult
    {
        public long Id { get; set; }
        public long TrustyDraftId { get; set; }    
        public long IssueId { get; set; }
        public long ProgressId { get; set; }
        public long NextStepId { get; set; }
    }
}
