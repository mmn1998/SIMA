using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategyStaff : Entity
{
    private BusinessContinuityStrategyStaff() { }
    private BusinessContinuityStrategyStaff(CreateBusinessContinuityStategyStaffArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityStrategyStaff Create(CreateBusinessContinuityStategyStaffArg arg)
    {
        return new BusinessContinuityStrategyStaff(arg);
    }
    public void Modify(ModifyBusinessContinuityStategyStaffArg arg)
    {
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessContinuityStrategyStaffId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStategy { get; private set; }
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