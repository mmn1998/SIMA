using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities
{
    public class BusinessContinuityPlanResponsible : Entity
    {
        private BusinessContinuityPlanResponsible() { }
        private BusinessContinuityPlanResponsible(CreateBusinessContinuityPlanResponsibleArg arg)
        {
            Id = new(IdHelper.GenerateUniqueId());
            BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
            StaffId = new(arg.StaffId);
            PlanResponsibilityId = new(arg.PlanResponsibilityId);
            IsForBackup = arg.IsForBackup;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static BusinessContinuityPlanResponsible Create(CreateBusinessContinuityPlanResponsibleArg arg)
        {
            return new BusinessContinuityPlanResponsible(arg);
        }

        public void ChangeStatus(ActiveStatusEnum status)
        {
            ActiveStatusId = (long)status;
        }

        public BusinessContinuityPlanResponsibleId Id { get; private set; }
        public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
        public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
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
    }
}
