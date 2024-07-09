using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
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
        BusinessContinuityStrategyId = new(arg.BusinessContinuityStrategyId);
        if (arg.PlanOwnerId.HasValue) PlanOwnerId = new(arg.PlanOwnerId.Value);
        if (arg.ExecutiveResponsibleId.HasValue) PlanOwnerId = new(arg.ExecutiveResponsibleId.Value);
        if (arg.RecoveryDeputyId.HasValue) PlanOwnerId = new(arg.RecoveryDeputyId.Value);
        if (arg.RecoveryManagerId.HasValue) PlanOwnerId = new(arg.RecoveryManagerId.Value);
        OfferDate = arg.OfferDate;
        Scope = arg.Scope;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
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
        BusinessContinuityStrategyId = new(arg.BusinessContinuityStrategyId);
        if (arg.PlanOwnerId.HasValue) PlanOwnerId = new(arg.PlanOwnerId.Value);
        if (arg.ExecutiveResponsibleId.HasValue) PlanOwnerId = new(arg.ExecutiveResponsibleId.Value);
        if (arg.RecoveryDeputyId.HasValue) PlanOwnerId = new(arg.RecoveryDeputyId.Value);
        if (arg.RecoveryManagerId.HasValue) PlanOwnerId = new(arg.RecoveryManagerId.Value);
        OfferDate = arg.OfferDate;
        Scope = arg.Scope;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {

    }
    #endregion
    public BusinessContinuityPlanId Id { get; private set; }
    public string? Code { get; private set; }
    public string? Title { get; private set; }
    public BusinessContinuityStrategyId BusinessContinuityStrategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
    public StaffId? PlanOwnerId { get; private set; }
    public virtual Staff? PlanOwner { get; private set; }
    public StaffId? ExecutiveResponsibleId { get; private set; }
    public virtual Staff? ExecutiveResponsible { get; private set; }
    public StaffId? RecoveryManagerId { get; private set; }
    public virtual Staff? RecoveryManager { get; private set; }
    public StaffId? RecoveryDeputyId { get; private set; }
    public virtual Staff? RecoveryDeputy { get; private set; }
    public DateTime OfferDate { get; private set; }
    public string? Scope { get; private set; }
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
    private List<BusinessContinuityPlanCriticalActivity> _businessContinuityPlanCriticalActivities = new();
    public ICollection<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities => _businessContinuityPlanCriticalActivities;
    private List<BusinessContinuityPlanDetailPlanningAssumption> _businessContinuityPlanDetailPlanningAssumptions = new();
    public ICollection<BusinessContinuityPlanDetailPlanningAssumption> BusinessContinuityPlanDetailPlanningAssumptions => _businessContinuityPlanDetailPlanningAssumptions;
    private List<BusinessContinuityPlanPossibleAction> _businessContinuityPlanPossibleActions = new();
    public ICollection<BusinessContinuityPlanPossibleAction> BusinessContinuityPlanPossibleActions => _businessContinuityPlanPossibleActions;
    private List<BusinessContinuityPlanRecoveryCriteria> _businessContinuityPlanRecoveryCriterias = new();
    public ICollection<BusinessContinuityPlanRecoveryCriteria> BusinessContinuityPlanRecoveryCriterias => _businessContinuityPlanRecoveryCriterias;
    private List<BusinessContinuityPlanRecoveryOption> _businessContinuityPlanRecoveryOptions = new();
    public ICollection<BusinessContinuityPlanRecoveryOption> BusinessContinuityPlanRecoveryOptions => _businessContinuityPlanRecoveryOptions;
    private List<BusinessContinuityPlanRisk> _businessContinuityPlanRisks = new();
    public ICollection<BusinessContinuityPlanRisk> BusinessContinuityPlanRisks => _businessContinuityPlanRisks;
    private List<BusinessContinuityPlanService> _businessContinuityPlanServices = new();
    public ICollection<BusinessContinuityPlanService> BusinessContinuityPlanServices => _businessContinuityPlanServices;
    private List<BusinessContinuityPlanStaff> _businessContinuityPlanStaff = new();
    public ICollection<BusinessContinuityPlanStaff> BusinessContinuityPlanStaff => _businessContinuityPlanStaff;
}
