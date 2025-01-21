using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

public class TrustyDraftDocument : Entity
{
    private TrustyDraftDocument()
    {

    }
    private TrustyDraftDocument(CreateTrustyDraftDocumentArg arg)
    {
        Id = new(arg.Id);
        TrustyDraftId = new(arg.TrustyDraftId);
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
    }
    public static TrustyDraftDocument Create(CreateTrustyDraftDocumentArg arg)
    {
        return new TrustyDraftDocument(arg);
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public TrustyDraftDocumentId Id { get; private set; }
    public TrustyDraftId TrustyDraftId { get; private set; }
    public virtual TrustyDraft TrustyDraft { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<PaymentReceiptInfo> _paymentReceiptInfos = new();
    public ICollection<PaymentReceiptInfo> PaymentReceiptInfos => _paymentReceiptInfos;

    private List<ReferralLetter> _letterDocuments = new();
    public ICollection<ReferralLetter> LetterDocuments => _letterDocuments;

    private List<Statement> _statements = new();
    public ICollection<Statement> Statements => _statements;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
