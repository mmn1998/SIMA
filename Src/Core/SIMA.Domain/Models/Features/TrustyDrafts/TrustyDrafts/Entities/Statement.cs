using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities
{
    public class Statement : Entity
    {
        public StatementId Id { get;  private set; }
        public DateTime StatementDate { get;  private set; }
        public string Description { get;  private set; }

        public TrustyDraftId TrustyDraftId { get; private set; }
        public virtual TrustyDraft TrustyDraft { get; private set; }
        public TrustyDraftDocumentId TrustyDocumentId { get;  private set; }
        public virtual TrustyDraftDocument TrustyDocument { get;  private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

    }
}
