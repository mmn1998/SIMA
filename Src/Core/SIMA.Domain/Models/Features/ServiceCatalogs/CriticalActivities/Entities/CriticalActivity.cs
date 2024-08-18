using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivity : Entity
{
    private CriticalActivity()
    {
    }

    private CriticalActivity(CreateCriticalActivityArg arg)
    {
        Id = new CriticalActivityId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.TechnicalSupervisorDepartmentId.HasValue) TechnicalSupervisorDepartmentId = new DepartmentId(arg.TechnicalSupervisorDepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<CriticalActivity> Create(CreateCriticalActivityArg arg)
    {
        return new CriticalActivity(arg);
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public CriticalActivityId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public virtual Department TechnicalSupervisorDepartment { get; private set; }
    public DepartmentId? TechnicalSupervisorDepartmentId { get; private set; }

    private List<CriticalActivityAssignedStaff> _criticalActivityAssignStaffs = new();
    public ICollection<CriticalActivityAssignedStaff> CriticalActivityAssignStaffs => _criticalActivityAssignStaffs;

    private List<CriticalActivityAsset> _criticalActivityAssets = new();
    public ICollection<CriticalActivityAsset> CriticalActivityAssets => _criticalActivityAssets;

    private List<CriticalActivityRisk> _criticalActivityRisks = new();
    public ICollection<CriticalActivityRisk> CriticalActivityRisks => _criticalActivityRisks;
    private List<CriticalActivityExecutionPlan> _criticalActivityExecutionPlans = new();
    public ICollection<CriticalActivityExecutionPlan> CriticalActivityExecutionPlans => _criticalActivityExecutionPlans;
    private List<CriticalActivityServices> _criticalActivityServices = new();
    public ICollection<CriticalActivityServices> CriticalActivityServices => _criticalActivityServices;
    private List<BusinessImpactAnalysisCriticalActivity> _businessImpactAnalysisCriticalActivities = new();
    public ICollection<BusinessImpactAnalysisCriticalActivity> BusinessImpactAnalysisCriticalActivities => _businessImpactAnalysisCriticalActivities;

    private List<BusinessContinuityPlanCriticalActivity> _businessContinuityPlanCriticalActivity = new();
    public ICollection<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities => _businessContinuityPlanCriticalActivity;
}
