using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

public class PaymentReceiptInfo : Entity
{
    public PaymentReceiptInfoId Id { get; private set; }
    public TrustyDraftId TrustyDraftId { get; private set; }
    public virtual TrustyDraft TrustyDraft { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public double PaymentAmount { get; private set; }
    public TrustyDraftDocumentId TrustyDraftDocumentId { get; private set; }
    public virtual TrustyDraftDocument TrustyDraftDocument { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
