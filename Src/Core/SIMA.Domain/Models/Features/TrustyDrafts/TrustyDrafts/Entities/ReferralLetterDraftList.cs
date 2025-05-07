using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities
{
    public class ReferralLetterDraftList : Entity
    {
        public ReferralLetterDraftListId Id { get;  private set; }
        public TrustyDraftId TrustyDraftId { get;  private set; }
        public virtual TrustyDraft TrustyDraft { get;  private set; }
        public ReferralLetterId ReferralLetterId { get;  private set; }
        public virtual ReferralLetter ReferralLetter { get;  private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
