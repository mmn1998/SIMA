using SIMA.Domain.Models.Features.TrustyDrafts.Reconsilations.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.Reconsilations.Entities;

public class Reconsilation : Entity
{
    public ReconsilationId Id { get;  private set; }
    public TrustyDraftId TrustyDraftId { get;  private set; }
    public virtual TrustyDraft TrustyDraft { get;  private set; }
    public ReconsilationTypeId ReconsilationTypeId { get;  private set; }
    public virtual ReconsilationType ReconsilationType { get;  private set; }
    //public BrokerId? BrokerId { get;  private set; }
    //public virtual Broker? Broker { get;  private set; }
    //public BranchId? BranchId { get;  private set; }
    //public virtual Branch? Branch { get;  private set; }
    //public string PartNumber { get;  private set; }
    //public decimal DraftAmount { get;  private set; }
    //public decimal DraftAmountBaseCurrency { get;  private set; }
    //public decimal WageAmount { get;  private set; }
    //public decimal WageAmountBaseCurrency { get;  private set; }
    //public decimal TaxAmount { get;  private set; }
    //public decimal TaxAmountBaseCurrency { get;  private set; }
    //public decimal NetAmount { get;  private set; }
    //public decimal NetAmountBaseCurrency { get;  private set; }
    //public string SwiftMessageCode { get;  private set; }
    //public string SwiftMessage { get;  private set; }
    public string? Description { get;  private set; }
    public string? IsInformedByBranch { get;  private set; }
    //public string LetterNumber { get;  private set; }
    //public DateTime LetterDate { get;  private set; }
    public DateTime InformedDate { get;  private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
