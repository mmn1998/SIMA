using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStategyStaff : Entity
{
    private BusinessContinuityStategyStaff() { }
    private BusinessContinuityStategyStaff(CreateBusinessContinuityStategyStaffArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityStategyStaff Create(CreateBusinessContinuityStategyStaffArg arg)
    {
        return new BusinessContinuityStategyStaff(arg);
    }
    public void Modify(ModifyBusinessContinuityStategyStaffArg arg)
    {
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessContinuityStategyStaffId Id { get; set; }
    public BusinessContinuityStategyId BusinessContinuityStategyId { get; private set; }
    public StaffId StaffId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
