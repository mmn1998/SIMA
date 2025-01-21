using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanRelatedStaff : Entity
{
    private BusinessContinuityPlanRelatedStaff() { }
    private BusinessContinuityPlanRelatedStaff(CreateBusinessContinuityPlanRelatedStaffArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanRelatedStaff Create(CreateBusinessContinuityPlanRelatedStaffArg arg)
    {
        return new BusinessContinuityPlanRelatedStaff(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanRelatedStaffArg arg)
    {
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public BusinessContinuityPlanRelatedStaffId Id { get; private set; }
    public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
    public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
    public StaffId StaffId { get; private set; }
    public virtual Staff Staff { get; private set; }
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
}
