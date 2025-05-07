using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;

public class BusinessContinuityStratgyResponsible : Entity
{
    private BusinessContinuityStratgyResponsible()
    {

    }
    private BusinessContinuityStratgyResponsible(CreateBusinessContinuityStratgyResponsibleArg arg)
    {
        Id = new(arg.Id);
        BusinessContinuityStrategyId = new(arg.BusinessContinuityStrategyId);
        StaffId = new(arg.StaffId);
        PlanResponsibilityId = new(arg.PlanResponsibilityId);
        IsForBackup = arg.IsForBackup;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static BusinessContinuityStratgyResponsible Create(CreateBusinessContinuityStratgyResponsibleArg arg)
    {
        return new BusinessContinuityStratgyResponsible(arg);
    }
    public void Modify(ModifyBusinessContinuityStratgyResponsibleArg arg)
    {
        BusinessContinuityStrategyId = new(arg.BusinessContinuityStrategyId);
        StaffId = new (arg.StaffId);
        PlanResponsibilityId = new (arg.PlanResponsibilityId);
        IsForBackup = arg.IsForBackup;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessContinuityStratgyResponsibleId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStrategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
    public StaffId StaffId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public PlanResponsibilityId PlanResponsibilityId { get; private set; }
    public virtual PlanResponsibility PlanResponsibility { get; private set; }
    public BranchId? BranchId { get; private set; }
    public virtual Branch? Branch { get; private set; }
    public DepartmentId? DepartmentId { get; private set; }
    public virtual Department? Department { get; private set; }
    public string IsForBackup { get; private set; }
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
