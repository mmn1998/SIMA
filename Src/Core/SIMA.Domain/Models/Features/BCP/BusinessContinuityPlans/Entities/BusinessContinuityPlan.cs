using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Events;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Entities;
using SIMA.Domain.Models.Features.BCP.PlanTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlan : Entity, IAggregateRoot
{
    private BusinessContinuityPlan() { }
    private BusinessContinuityPlan(CreateBusinessContinuityPlanArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        Title = arg.Title;
        Scope = arg.Scope;
        VersionNumber = arg.VersionNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ReleaseDate = arg.ReleaseDate;
    }
    public static BusinessContinuityPlan CreateEmpty()
    {
        return new BusinessContinuityPlan();
    }
    public static async Task<BusinessContinuityPlan> Create(CreateBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessContinuityPlan(arg);
    }
    public async Task Modify(ModifyBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Title = arg.Title;
        Scope = arg.Scope;
        VersionNumber = arg.VersionNumber;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ReleaseDate = arg.ReleaseDate;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.Scope.NullCheck();
        arg.VersionNumber.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsVersionUnique(arg.VersionNumber)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueVersionError);
    }
    private async Task ModifyGuards(ModifyBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        //arg.Code.NullCheck();
        arg.Scope.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsVersionUnique(arg.VersionNumber, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueVersionError);
    }
    #endregion

    #region OtherMethod

    //public void AddBusinessContinuityPlanVersioning(CreateBusinessContinuityPlanVersioningArg request)
    //{
    //    var entity = BusinessContinuityPlanVersioning.Create(request);
    //    _businessContinuityPlanVersioning.Add(entity);
    //}

    //public void AddBusinessContinuityPlanStratgy(List<CreateBusinessContinuityPlanStratgyArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanStratgy(request, versionId);
    //}

    //public void AddBusinessContinuityPlanService(List<CreateBusinessContinuityPlanServiceArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanService(request, versionId);
    //}

    //public void AddBusinessContinuityPlanRisk(List<CreateBusinessContinuityPlanRiskArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanRisk(request, versionId);
    //}

    //public void AddBusinessContinuityPlanRelatedStaff(List<CreateBusinessContinuityPlanRelatedStaffArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanStaff(request, versionId);
    //}

    //public void AddBusinessContinuityPlanCriticalActivity(List<CreateBusinessContinuityPlanCriticalActivityArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanCriticalActivity(request, versionId);
    //}

    //public void AddBusinessContinuityPlanResponsible(List<CreateBusinessContinuityPlanResponsibleArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanResponsible(request, versionId);
    //}

    //public void AddBusinessContinuityPlanAssumption(List<CreateBusinessContinuityPlanAssumptionArg> request, long versionId)
    //{
    //    var entity = _businessContinuityPlanVersioning.Where(x => x.Id.Value == versionId).FirstOrDefault();
    //    entity.AddBusinessContinuityPlanAssumption(request, versionId);
    //}

    public void AddBusinessContinuityPlanIssue(CreateBusinessContinuityPlanIssueArg request, long planId, string title)
    {
        AddDomainEvent(new CreateBusinessContinuityPlanEvent(request.IssueId, MainAggregateEnums.BusinessContinuityPlan, title, planId));
        request.BusinessContinuityPlanId = planId;
        var entity = BusinessContinuityPlanIssue.Create(request);
        _businessContinuityPlanIssues.Add(entity);
    }

    //public async Task AddBusinessContinuityPlanStratgy(List<CreateBusinessContinuityPlanStratgyArg> request, long planId)
    //{
    //    scenarioId.NullCheck();

    //    var previousEntity = _.Where(x => x.BusinessContinuityPlanId == new BusinessContinuityPlanId(planId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

    //    var addEntity = request.Where(x => !previousEntity.Any(c => c.BusinessContinuityPlanVersioningId.Value == x.BusinessContinuityPlanVersioningId)).ToList();
    //    var deleteEntity = previousEntity.Where(x => !request.Any(c => c.BusinessContinuityPlanVersioningId == x.BusinessContinuityPlanVersioningId.Value)).ToList();


    //    foreach (var item in addEntity)
    //    {
    //        var entity = _scenarioBusinessContinuityPlanVersioning.Where(x => (x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(item.BusinessContinuityPlanVersioningId) && x.ScenarioId == new ScenarioId(scenarioId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
    //        if (entity is not null)
    //        {
    //            entity.ChangeStatus(ActiveStatusEnum.Active);
    //        }
    //        else
    //        {
    //            entity = ScenarioBusinessContinuityPlanVersioning.Create(item);
    //            _scenarioBusinessContinuityPlanVersioning.Add(entity);
    //        }
    //    }

    //    foreach (var item in deleteEntity)
    //    {
    //        item.Delete((long)request[0].CreatedBy);
    //    }
    //}

    //public async Task AddBusinessContinuityPlanService(List<CreateBusinessContinuityPlanServiceArg> request, long planId)
    //{
    //    planId.NullCheck();

    //    var previousEntity = _.Where(x => x.BusinessContinuityPlanId == new BusinessContinuityPlanId(planId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

    //    var addEntity = request.Where(x => !previousEntity.Any(c => c.BusinessContinuityPlanVersioningId.Value == x.BusinessContinuityPlanVersioningId)).ToList();
    //    var deleteEntity = previousEntity.Where(x => !request.Any(c => c.BusinessContinuityPlanVersioningId == x.BusinessContinuityPlanVersioningId.Value)).ToList();


    //    foreach (var item in addEntity)
    //    {
    //        var entity = _scenarioBusinessContinuityPlanVersioning.Where(x => (x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(item.BusinessContinuityPlanVersioningId) && x.ScenarioId == new ScenarioId(scenarioId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
    //        if (entity is not null)
    //        {
    //            entity.ChangeStatus(ActiveStatusEnum.Active);
    //        }
    //        else
    //        {
    //            entity = ScenarioBusinessContinuityPlanVersioning.Create(item);
    //            _scenarioBusinessContinuityPlanVersioning.Add(entity);
    //        }
    //    }

    //    foreach (var item in deleteEntity)
    //    {
    //        item.Delete((long)request[0].CreatedBy);
    //    }
    //}

    #endregion

    public BusinessContinuityPlanId Id { get; private set; }
    public string Code { get; private set; }
    public string Title { get; private set; }
    public string Scope { get; private set; }
    public string? VersionNumber { get; private set; }
    public PlanTypeId? PlanTypeId { get; private set; }
    public virtual PlanType? PlanType { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Update(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    //public void DeleteVersion(long userId, long versionid)
    //{
    //    var version = _businessContinuityPlanVersioning.Where(x => x.Id == new BusinessContinuityPlanVersioningId(versionid)).FirstOrDefault();
    //    version.Delete(userId);
    //}


    private List<BusinessContinuityPlanAssumption> _businessContinuityPlanDetailPlanningAssumptions = new();
    public ICollection<BusinessContinuityPlanAssumption> BusinessContinuityPlanAssumptions => _businessContinuityPlanDetailPlanningAssumptions;

    private List<BusinessContinuityPlanPossibleAction> _businessContinuityPlanPossibleActions = new();
    public ICollection<BusinessContinuityPlanPossibleAction> BusinessContinuityPlanPossibleActions => _businessContinuityPlanPossibleActions;

    private List<BusinessContinuityPlanRecoveryCriteria> _businessContinuityPlanRecoveryCriterias = new();
    public ICollection<BusinessContinuityPlanRecoveryCriteria> BusinessContinuityPlanRecoveryCriterias => _businessContinuityPlanRecoveryCriterias;

    private List<BusinessContinuityPlanRecoveryOption> _businessContinuityPlanRecoveryOptions = new();
    public ICollection<BusinessContinuityPlanRecoveryOption> BusinessContinuityPlanRecoveryOptions => _businessContinuityPlanRecoveryOptions;

    private List<BusinessContinuityPlanIssue> _businessContinuityPlanIssues = new();
    public ICollection<BusinessContinuityPlanIssue> BusinessContinuityPlanIssues => _businessContinuityPlanIssues;

    private List<BusinessContinuityPlanStratgy> _businessContinuityPlanStratgies = new();
    public ICollection<BusinessContinuityPlanStratgy> BusinessContinuityPlanStratgies => _businessContinuityPlanStratgies;

    private List<BusinessContinuityPlanResponsible> _businessContinuityPlanResponsibles = new();
    public ICollection<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles => _businessContinuityPlanResponsibles;

    private List<BusinessContinuityPlanService> _businessContinuityPlanServices = new();
    public ICollection<BusinessContinuityPlanService> BusinessContinuityPlanServices => _businessContinuityPlanServices;

    private List<BusinessContinuityPlanRisk> _businessContinuityPlanRisks = new();
    public ICollection<BusinessContinuityPlanRisk> BusinessContinuityPlanRisks => _businessContinuityPlanRisks;

    private List<BusinessContinuityPlanCriticalActivity> _businessContinuityPlanCriticalActivities = new();
    public ICollection<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities => _businessContinuityPlanCriticalActivities;

    private List<BusinessContinuityPlanRelatedStaff> _businessContinuityPlanRelatedStaff = new();
    public ICollection<BusinessContinuityPlanRelatedStaff> BusinessContinuityPlanRelatedStaff => _businessContinuityPlanRelatedStaff;
}
