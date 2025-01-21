using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;

public class ReferralLetter : Entity
{
    private ReferralLetter()
    {

    }
    private ReferralLetter(CreateReferalLetterArg arg)
    {
        Id = new(arg.Id);
        LetterNumber = arg.LetterNumber;
        LetterDate = arg.LetterDate;
        BrokerId = new(arg.BrokerId);
        TrustyDraftId = new(arg.TrustyDraftId);
        if (arg.LetterDocumentId.HasValue)
            LetterDocumentId = new(arg.LetterDocumentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ReferralLetter> Create(CreateReferalLetterArg arg)
    {
        return new ReferralLetter(arg);
    }
    public async Task Modify(ModifyReferalLetterArg arg)
    {
        LetterNumber = arg.LetterNumber;
        LetterDate = arg.LetterDate;
        BrokerId = new(arg.BrokerId);
        TrustyDraftId = new(arg.TrustyDraftId);
        if (arg.LetterDocumentId.HasValue) LetterDocumentId = new(arg.LetterDocumentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public ReferralLetterId Id { get; private set; }
    public string LetterNumber { get; private set; }
    public DateTime LetterDate { get; private set; }
    public BrokerId BrokerId { get; private set; }
    public virtual Broker Broker { get; private set; }
    public TrustyDraftDocumentId? LetterDocumentId { get; private set; }
    public virtual TrustyDraftDocument? LetterDocument { get; private set; }
    public TrustyDraftId? TrustyDraftId { get; private set; }
    public virtual TrustyDraft? TrustyDraft { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ReferralLetterDraftList> _referralLetterDraftList = new();
    public ICollection<ReferralLetterDraftList> ReferralLetterDraftList => _referralLetterDraftList;

}
