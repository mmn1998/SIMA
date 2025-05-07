using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

public class TrustyDraftResource : Entity
{
    private TrustyDraftResource()
    {

    }
    private TrustyDraftResource(CreateTrustyDraftResourceArg arg)
    {
        Id = new(arg.Id);
        TrustyDraftId = new(arg.TrustyDraftId);
        ResourceId = new(arg.ResourceId);
        AssignedAmount = arg.AssignedAmount;
        AssignedDate = arg.AssignedDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
    }
    public static TrustyDraftResource Create(CreateTrustyDraftResourceArg arg)
    {
        return new TrustyDraftResource(arg);
    }
    public TrustyDraftResourceId Id { get; private set; }
    public TrustyDraftId TrustyDraftId { get; private set; }
    public virtual TrustyDraft TrustyDraft { get; private set; }
    public ResourceId ResourceId { get; private set; }
    public virtual Resource Resource { get; private set; }
    public decimal AssignedAmount { get; private set; }
    public DateTime AssignedDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
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
