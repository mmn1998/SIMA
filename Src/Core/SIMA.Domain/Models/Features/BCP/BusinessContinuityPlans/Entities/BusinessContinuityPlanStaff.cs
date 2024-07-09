using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanStaff : Entity
{
    private BusinessContinuityPlanStaff() { }
    private BusinessContinuityPlanStaff(CreateBusinessContinuityPlanStaffArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanStaff Create(CreateBusinessContinuityPlanStaffArg arg)
    {
        return new BusinessContinuityPlanStaff(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanStaffArg arg)
    {
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessContinuityPlanStaffId Id { get; private set; }
    public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
    public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
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
