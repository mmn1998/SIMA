using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Events;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivity : Entity, IAggregateRoot
{
    private CriticalActivity()
    {
    }

    private CriticalActivity(CreateCriticalActivityArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.TechnicalSupervisorDepartmentId.HasValue) TechnicalSupervisorDepartmentId = new(arg.TechnicalSupervisorDepartmentId.Value);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        IssueId = new(arg.IssueId);
        AddDomainEvent(new CreateCriticalActivityEvent(arg.IssueId, MainAggregateEnums.CriticalActivity, arg.Name, arg.Id));
    }

    public static async Task<CriticalActivity> Create(CreateCriticalActivityArg arg, ICriticalActivityDomainService service)
    {
        await CreateGuards(arg, service);
        return new CriticalActivity(arg);
    }
    public async Task Modify(ModifyCriticalActivityArg arg, ICriticalActivityDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        if (arg.TechnicalSupervisorDepartmentId.HasValue) TechnicalSupervisorDepartmentId = new(arg.TechnicalSupervisorDepartmentId.Value);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        //AddDomainEvent(new ModifyCriticalActivityEvent(IssueId.Value, MainAggregateEnums.CriticalActivity, arg.Name, Id.Value));
    }
    #region Guards
    private static async Task CreateGuards(CreateCriticalActivityArg arg, ICriticalActivityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyCriticalActivityArg arg, ICriticalActivityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    } 
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRelatedEntities
        DeleteAssignedStaffs(userId);
        DeleteCriticalActivityAssets(userId);
        DeleteCriticalActivityConfigurationItems(userId);
        DeleteCriticalActivityExecutionPlans(userId);
        DeleteCriticalActivityRisks(userId);
        DeleteCriticalActivityServices(userId);
        #endregion
        AddDomainEvent(new DeleteCriticalActivityEvent(IssueId.Value));
    }

    #region AddMethods
    public void AddAssignedStaffs(List<CreateCriticalActivityAssignedStaffArg> args)
    {
        foreach (var arg in args)
        {
            var entity = CriticalActivityAssignedStaff.Create(arg);
            _criticalActivityAssignStaffs.Add(entity);
        }
    }
    public void AddCriticalActivityAssets(List<CreateCriticalActivityAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = CriticalActivityAsset.Create(arg);
            _criticalActivityAssets.Add(entity);
        }
    }
    public void AddCriticalActivityRisks(List<CreateCriticalActivityRiskArg> args)
    {
        foreach (var arg in args)
        {
            var entity = CriticalActivityRisk.Create(arg);
            _criticalActivityRisks.Add(entity);
        }
    }
    public void AddCriticalActivityExrecutionPlans(List<CreateCriticalActivityExecutionPlanArg> args)
    {
        foreach (var arg in args)
        {
            var entity = CriticalActivityExecutionPlan.Create(arg);
            _criticalActivityExecutionPlans.Add(entity);
        }
    }
    public void AddCriticalActivityServices(List<CreateCriticalActivityServicesArg> args)
    {
        foreach (var arg in args)
        {
            var entity = CriticalActivityService.Create(arg);
            _criticalActivityServices.Add(entity);
        }
    }
    public void AddCriticalActivityConfigurationItems(List<CreateCriticalActivityConfigurationItemArg> args)
    {
        foreach (var arg in args)
        {
            var entity = CriticalActivityConfigurationItem.Create(arg);
            _criticalActivityConfigurationItems.Add(entity);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteAssignedStaffs(long userId)
    {
        foreach (var item in _criticalActivityAssignStaffs)
        {
            item.Delete(userId);
        }
    }
    public void DeleteCriticalActivityAssets(long userId)
    {
        foreach (var item in _criticalActivityAssets)
        {
            item.Delete(userId);
        }
    }
    public void DeleteCriticalActivityRisks(long userId)
    {
        foreach (var item in _criticalActivityRisks)
        {
            item.Delete(userId);
        }
    }
    public void DeleteCriticalActivityExecutionPlans(long userId)
    {
        foreach (var item in _criticalActivityExecutionPlans)
        {
            item.Delete(userId);
        }
    }
    public void DeleteCriticalActivityServices(long userId)
    {
        foreach (var item in _criticalActivityServices)
        {
            item.Delete(userId);
        }
    }
    public void DeleteCriticalActivityConfigurationItems(long userId)
    {
        foreach (var item in _criticalActivityConfigurationItems)
        {
            item.Delete(userId);
        }
    }
    #endregion
    #region ModifyMethods
    public void ModifyAssignedStaffs(List<CreateCriticalActivityAssignedStaffArg> args)
    {
        var activeEntities = _criticalActivityAssignStaffs.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ResponsibleTypeId == x.ResponsilbeTypeId.Value && c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ResponsilbeTypeId.Value == x.ResponsibleTypeId && c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _criticalActivityAssignStaffs.FirstOrDefault(x => x.ResponsilbeTypeId.Value == arg.ResponsibleTypeId && x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = CriticalActivityAssignedStaff.Create(arg);
                _criticalActivityAssignStaffs.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyCriticalActivityAssets(List<CreateCriticalActivityAssetArg> args)
    {
        var activeEntities = _criticalActivityAssets.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetId == x.AssetId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetId.Value == x.AssetId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _criticalActivityAssets.FirstOrDefault(x =>  x.AssetId.Value == arg.AssetId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = CriticalActivityAsset.Create(arg);
                _criticalActivityAssets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyCriticalActivityRisks(List<CreateCriticalActivityRiskArg> args)
    {
        var activeEntities = _criticalActivityRisks.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.RiskId == x.RiskId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.RiskId.Value == x.RiskId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _criticalActivityRisks.FirstOrDefault(x =>  x.RiskId.Value == arg.RiskId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = CriticalActivityRisk.Create(arg);
                _criticalActivityRisks.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyCriticalActivityExecutionPlans(List<CreateCriticalActivityExecutionPlanArg> args)
    {
        var activeEntities = _criticalActivityExecutionPlans.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.WeekDay == x.WeekDay && c.ServiceAvalibilityEndTime == x.ServiceAvalibilityEndTime && c.ServiceAvalibilityStartTime == x.ServiceAvalibilityStartTime));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.WeekDay == x.WeekDay && c.ServiceAvalibilityEndTime == x.ServiceAvalibilityEndTime && c.ServiceAvalibilityStartTime == x.ServiceAvalibilityStartTime));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _criticalActivityExecutionPlans.FirstOrDefault(x =>  x.WeekDay == arg.WeekDay && x.ServiceAvalibilityEndTime == arg.ServiceAvalibilityEndTime && x.ServiceAvalibilityStartTime == arg.ServiceAvalibilityStartTime && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = CriticalActivityExecutionPlan.Create(arg);
                _criticalActivityExecutionPlans.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyCriticalActivityServices(List<CreateCriticalActivityServicesArg> args)
    {
        var activeEntities = _criticalActivityServices.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceId == x.ServiceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceId.Value == x.ServiceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _criticalActivityServices.FirstOrDefault(x => x.ServiceId.Value == arg.ServiceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = CriticalActivityService.Create(arg);
                _criticalActivityServices.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyCriticalConfigurationItems(List<CreateCriticalActivityConfigurationItemArg> args)
    {
        var activeEntities = _criticalActivityConfigurationItems.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ConfigurationItemId == x.ConfigurationItemId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ConfigurationItemId.Value == x.ConfigurationItemId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _criticalActivityConfigurationItems.FirstOrDefault(x => x.ConfigurationItemId.Value == arg.ConfigurationItemId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = CriticalActivityConfigurationItem.Create(arg);
                _criticalActivityConfigurationItems.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion
    public CriticalActivityId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public virtual Department? TechnicalSupervisorDepartment { get; private set; }
    public DepartmentId? TechnicalSupervisorDepartmentId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public IssueId IssueId { get; private set; }

    private List<CriticalActivityAssignedStaff> _criticalActivityAssignStaffs = new();
    public ICollection<CriticalActivityAssignedStaff> CriticalActivityAssignStaffs => _criticalActivityAssignStaffs;

    private List<CriticalActivityAsset> _criticalActivityAssets = new();
    public ICollection<CriticalActivityAsset> CriticalActivityAssets => _criticalActivityAssets;

    private List<CriticalActivityRisk> _criticalActivityRisks = new();
    public ICollection<CriticalActivityRisk> CriticalActivityRisks => _criticalActivityRisks;
    private List<CriticalActivityExecutionPlan> _criticalActivityExecutionPlans = new();
    public ICollection<CriticalActivityExecutionPlan> CriticalActivityExecutionPlans => _criticalActivityExecutionPlans;
    private List<CriticalActivityService> _criticalActivityServices = new();
    public ICollection<CriticalActivityService> CriticalActivityServices => _criticalActivityServices;
    private List<CriticalActivityConfigurationItem> _criticalActivityConfigurationItems = new();
    public ICollection<CriticalActivityConfigurationItem> CriticalActivityConfigurationItems => _criticalActivityConfigurationItems;
    private List<BusinessImpactAnalysisCriticalActivity> _businessImpactAnalysisCriticalActivities = new();
    public ICollection<BusinessImpactAnalysisCriticalActivity> BusinessImpactAnalysisCriticalActivities => _businessImpactAnalysisCriticalActivities;

    private List<BusinessContinuityPlanCriticalActivity> _businessContinuityPlanCriticalActivity = new();
    public ICollection<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities => _businessContinuityPlanCriticalActivity;
}
