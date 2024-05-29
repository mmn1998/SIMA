using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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
        if (arg.DepartmentId.HasValue) DepartmentId = new DepartmentId(arg.DepartmentId.Value);
        if (arg.TechnicalSupervisorId.HasValue) TechnicalSupervisorId = new StaffId(arg.TechnicalSupervisorId.Value);
        if (arg.TechnicalResponsibleId.HasValue) TechnicalResponsibleId = new StaffId(arg.TechnicalResponsibleId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<CriticalActivity> Create(CreateCriticalActivityArg arg)
    {
        return new CriticalActivity(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
    public CriticalActivityId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    public virtual Staff? TechnicalResponsible { get; private set; }
    public StaffId? TechnicalResponsibleId { get; private set; }

    public virtual Staff? TechnicalSupervisor { get; private set; }
    public StaffId? TechnicalSupervisorId { get; private set; }

    public virtual Department? Department { get; private set; }
    public DepartmentId? DepartmentId { get; private set; }

    private List<CriticalActivityAssignStaff> _criticalActivityAssignStaffs = new();
    public ICollection<CriticalActivityAssignStaff> CriticalActivityAssignStaffs => _criticalActivityAssignStaffs;
    
    private List<CriticalActivityAsset> _criticalActivityAssets = new();
    public ICollection<CriticalActivityAsset> CriticalActivityAssets => _criticalActivityAssets;
    
    private List<CriticalActivityRisk> _criticalActivityRisks = new();
    public ICollection<CriticalActivityRisk> CriticalActivityRisks => _criticalActivityRisks;
    private List<CriticalActivityExecutionPlan> _criticalActivityExecutionPlans = new();
    public ICollection<CriticalActivityExecutionPlan> CriticalActivityExecutionPlans => _criticalActivityExecutionPlans;
    private List<CriticalActivityService> _criticalActivityServices = new();
    public ICollection<CriticalActivityService> CriticalActivityServices => _criticalActivityServices;
}
