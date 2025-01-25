using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Events;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Args;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

public class Risk : Entity
{
    private Risk()
    {

    }
    private Risk(CreateRiskArg arg)
    {
        Id = new(arg.Id);
        Code = arg.Code;
        Name = arg.Name;
        Description = arg.Description;
        RiskTypeId = new(arg.RiskTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<Risk> Create(CreateRiskArg arg, IRiskDomainService service)
    {
        await CreateGuards(arg, service);
        return new Risk(arg);
    }
    public async Task Modify(ModifyRiskArg arg, IRiskDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        Description = arg.Description;
        RiskTypeId = new RiskTypeId(arg.RiskTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        //AddDomainEvent(new ModifyRiskCreateEvents(_riskRelatedIssues.First().IssueId.Value, MainAggregateEnums.RiskManagment, Name, Id.Value));
    }
    #region Guards
    private static async Task CreateGuards(CreateRiskArg arg, IRiskDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyRiskArg arg, IRiskDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    #region AddMethods

    public void AddEffectAsset(CreateEffectedAssetArgs arg, List<CreateVulnerabilityArg> VulnerabilityArgs)
    {
        var effectAsset = EffectedAsset.Create(arg);
        foreach (var relatedArg in VulnerabilityArgs)
        {
            relatedArg.EffectedAssetId = effectAsset.Id.Value;
            relatedArg.CreatedBy = effectAsset.CreatedBy;
        }
        effectAsset.AddVulnerabilities(VulnerabilityArgs);
        _effectedAssets.Add(effectAsset);

    }
    public void AddServiceRiskImpact(CreateServiceRiskArg arg, List<CreateServiceRiskImpactArg> serviceRiskImpactArgs)
    {
        var entity = ServiceRisk.Create(arg);
        foreach (var serviceImpactArg in serviceRiskImpactArgs)
        {
            serviceImpactArg.ServiceRiskId = entity.Id.Value;
            serviceImpactArg.CreatedBy = arg.CreatedBy;
        }
        entity.AddServiceRiskImpacts(serviceRiskImpactArgs);
        _serviceRisks.Add(entity);
    }
    public void AddCorrectiveActions(List<CreateCorrectiveActionArg> args)
    {
        var correctives = args.Select(CorrectiveAction.Create);
        _correctiveActions.AddRange(correctives);
    }
    public void AddStaffs(List<CreateRiskStaffArg> args)
    {
        var staffs = args.Select(RiskStaff.Create);
        _riskStaffs.AddRange(staffs);
    }

    public void AddPreventiveActions(List<CreatePreventiveActionArg> args)
    {
        var preventives = args.Select(PreventiveAction.Create);
        _preventiveActions.AddRange(preventives);
    }

    public void AddRiskRelatedIssue(CreateRiskRelatedIssueArg arg)
    {
        var riskRelated = RiskRelatedIssue.Create(arg);
        _riskRelatedIssues.Add(riskRelated);

        AddDomainEvent(new RiskCreateEvents(arg.IssueId, MainAggregateEnums.RiskManagment, Name, Id.Value));
    }
    public void AddThreats(List<CreateThreatArg> args)
    {
        foreach (var arg in args)
        {
            var entity = Threat.Create(arg);
            _threats.Add(entity);
        }
    }
    #endregion
    #region ModifyMethods
    public void ModifyEffectAssets(List<ModifyEffectedAssetArgs> args)
    {
        var activeEntities = _effectedAssets.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetId == x.AssetId.Value && c.AV == x.AV && c.EF == x.EF && c.Sle == x.SLE && c.Ale == x.ALE));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetId.Value == x.AssetId && c.AV == x.AV && c.EF == x.EF && c.SLE == x.Sle && c.ALE == x.Ale));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _effectedAssets.FirstOrDefault(x => x.AssetId.Value == arg.AssetId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = EffectedAsset.Create(new CreateEffectedAssetArgs
                {
                    ActiveStatusId = arg.ActiveStatusId,
                    Ale = arg.Ale,
                    AssetId = arg.AssetId,
                    AV = arg.AV,
                    CreatedAt = arg.CreatedAt,
                    CreatedBy = arg.CreatedBy,
                    EF = arg.EF,
                    Id = arg.Id,
                    RiskId = arg.RiskId,
                    Sle = arg.Sle
                }
                );
                if (arg.VulnerabilityList is not null && arg.VulnerabilityList.Count > 0)
                {
                    foreach (var item in arg.VulnerabilityList)
                    {
                        item.CreatedBy = arg.CreatedBy;
                        item.EffectedAssetId = arg.Id;
                    }
                    entity.AddVulnerabilities(arg.VulnerabilityList);

                }
                _effectedAssets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    public void ModifyServiceRiskImpacts(List<ModifyServiceRiskArg> args)
    {
        var activeEntities = _serviceRisks.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceId == x.ServiceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceId.Value == x.ServiceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceRisks.FirstOrDefault(x => x.ServiceId.Value == arg.ServiceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceRisk.Create(new CreateServiceRiskArg
                {
                    ActiveStatusId = arg.ActiveStatusId,
                    CreatedAt = arg.CreatedAt,
                    CreatedBy = arg.CreatedBy,
                    Id = arg.Id,
                    RiskId = arg.RiskId,
                    ServiceId = arg.ServiceId
                }
                );
                if (arg.ServiceRiskImpacts is not null)
                {
                    foreach (var item in arg.ServiceRiskImpacts)
                    {
                        item.CreatedBy = arg.CreatedBy;
                        item.ServiceRiskId = arg.Id;
                    }
                    entity.AddServiceRiskImpacts(arg.ServiceRiskImpacts);

                }
                _serviceRisks.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyCorrectiveActions(List<CreateCorrectiveActionArg> args)
    {
        var activeEntities = _correctiveActions.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ActionDescription == x.ActionDescription));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ActionDescription == x.ActionDescription));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _correctiveActions.FirstOrDefault(x => x.ActionDescription == arg.ActionDescription && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = CorrectiveAction.Create(arg);
                _correctiveActions.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    public void ModifyStaffs(List<CreateRiskStaffArg> args)
    {
        var activeEntities = _riskStaffs.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _riskStaffs.FirstOrDefault(x => x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = RiskStaff.Create(arg);
                _riskStaffs.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    public void ModifyPreventiveActions(List<CreatePreventiveActionArg> args)
    {
        var activeEntities = _preventiveActions.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ActionDescription == x.ActionDescription));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ActionDescription == x.ActionDescription));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _preventiveActions.FirstOrDefault(x => x.ActionDescription == arg.ActionDescription && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = PreventiveAction.Create(arg);
                _preventiveActions.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    public void ModifyThreats(List<CreateThreatArg> args)
    {
        var activeEntities = _threats.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ThreatTypeId == x.ThreatTypeId.Value && c.RiskPossibilityId == x.RiskPossibilityId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ThreatTypeId.Value == x.ThreatTypeId && c.RiskPossibilityId.Value == x.RiskPossibilityId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _threats.FirstOrDefault(x => x.ThreatTypeId.Value == arg.ThreatTypeId && x.RiskPossibilityId.Value == arg.RiskPossibilityId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = Threat.Create(arg);
                _threats.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteEffectedAssets(long userId)
    {
        foreach (var item in _effectedAssets)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceRiskImpacts(long userId)
    {
        foreach (var item in _serviceRisks)
        {
            item.Delete(userId);
        }
    }
    public void DeleteCorrectiveActions(long userId)
    {
        foreach (var item in _correctiveActions)
        {
            item.Delete(userId);
        }
    }
    public void DeletePreventiveActions(long userId)
    {
        foreach (var item in _preventiveActions)
        {
            item.Delete(userId);
        }
    }
    public void DeleteThreats(long userId)
    {
        foreach (var item in _threats)
        {
            item.Delete(userId);
        }
    }
    public void DeleteIssues(long userId)
    {
        foreach (var item in _riskRelatedIssues)
        {
            item.Delete(userId);
        }
    }
    public void DeleteStaffs(long userId)
    {
        foreach (var item in _riskStaffs)
        {
            item.Delete(userId);
        }
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        DeleteCorrectiveActions(userId);
        DeleteEffectedAssets(userId);
        DeletePreventiveActions(userId);
        DeleteServiceRiskImpacts(userId);
        DeleteThreats(userId);
        DeleteStaffs(userId);
        AddDomainEvent(new DeleteRiskCreateEvents(_riskRelatedIssues.First().IssueId.Value));
        DeleteIssues(userId);
    }
    public RiskId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public RiskTypeId RiskTypeId { get; private set; }
    public virtual RiskType RiskType { get; private set; }
    public AffectedHistoryId? AffectedHistoryId { get; private set; }
    public virtual AffectedHistory? AffectedHistory { get; private set; }
    public UseVulnerabilityId? UseVulnerabilityId { get; private set; }
    public virtual UseVulnerability? UseVulnerability { get; private set; }
    public ConsequenceCategoryId? ConsequenceCategoryId { get; private set; }
    public virtual ConsequenceCategory? ConsequenceCategory { get; private set; }
    public TriggerStatusId? TriggerStatusId { get; private set; }
    public virtual TriggerStatus? TriggerStatus { get; private set; }
    public ScenarioHistoryId? ScenarioHistoryId { get; private set; }
    public virtual ScenarioHistory? ScenarioHistory { get; private set; }
    public FrequencyId? FrequencyId { get; private set; }
    public virtual Frequency? Frequency { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<CorrectiveAction> _correctiveActions = new();
    public ICollection<CorrectiveAction> CorrectiveActions => _correctiveActions;
    private List<PreventiveAction> _preventiveActions = new();
    public ICollection<PreventiveAction> PreventiveActions => _preventiveActions;
    private List<EffectedAsset> _effectedAssets = new();
    public ICollection<EffectedAsset> EffectedAssets => _effectedAssets;
    private List<RiskRelatedIssue> _riskRelatedIssues = new();
    public ICollection<RiskRelatedIssue> RiskRelatedIssues => _riskRelatedIssues;
    private List<Threat> _threats = new();
    public ICollection<Threat> Threats => _threats;
    private List<BusinessContinuityStrategyRisk> _businessContinuityStrategyRisks = new();
    public ICollection<BusinessContinuityStrategyRisk> BusinessContinuityStrategyRisks => _businessContinuityStrategyRisks;
    private List<BusinessContinuityPlanRisk> _businessContinuityPlanRisks = new();
    public ICollection<BusinessContinuityPlanRisk> BusinessContinuityPlanRisks => _businessContinuityPlanRisks;
    private List<CriticalActivityRisk> _criticalActivityRisks = new();
    public ICollection<CriticalActivityRisk> CriticalActivityRisks => _criticalActivityRisks;
    private List<ServiceRisk> _serviceRisks = new();
    public ICollection<ServiceRisk> ServiceRisks => _serviceRisks;
    private List<RiskStaff> _riskStaffs = new();
    public ICollection<RiskStaff> RiskStaffs => _riskStaffs;
    private List<RiskValueStrategy> _riskValueStrategies = new();
    public ICollection<RiskValueStrategy> RiskValueStrategies => _riskValueStrategies;
}
