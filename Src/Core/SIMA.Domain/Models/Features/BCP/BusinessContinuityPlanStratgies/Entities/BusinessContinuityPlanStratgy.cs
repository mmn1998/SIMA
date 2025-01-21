using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;

public class BusinessContinuityPlanStratgy : Entity
{
    private BusinessContinuityPlanStratgy()
    {

    }
    private BusinessContinuityPlanStratgy(CreateBusinessContinuityPlanStratgyArg arg)
    {
        Id = new BusinessContinuityPlanStratgyId(arg.Id);
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        BusinessContinuityStratgyId = new(arg.BusinessContinuityStratgyId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static BusinessContinuityPlanStratgy Create(CreateBusinessContinuityPlanStratgyArg arg)
    {
        return new BusinessContinuityPlanStratgy(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanStratgyArg arg)
    {
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        BusinessContinuityStratgyId = new(arg.BusinessContinuityStratgyId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public BusinessContinuityPlanStratgyId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStratgyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
    public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
    public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
