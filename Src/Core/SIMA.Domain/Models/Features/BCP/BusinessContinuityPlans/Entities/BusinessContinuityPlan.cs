using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
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
        Scope = arg.Scope;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.Scope.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyBusinessContinuityPlanArg arg, IBusinessContinuityPlanDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.Scope.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public BusinessContinuityPlanId Id { get; private set; }
    public string Code { get; private set; }
    public string Title { get; private set; }
    public string Scope { get; private set; }
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


    private List<BusinessContinuityPlanAssumption> _businessContinuityPlanDetailPlanningAssumptions = new();
    public ICollection<BusinessContinuityPlanAssumption> BusinessContinuityPlanAssumptions => _businessContinuityPlanDetailPlanningAssumptions;

    private List<BusinessContinuityPlanPossibleAction> _businessContinuityPlanPossibleActions = new();
    public ICollection<BusinessContinuityPlanPossibleAction> BusinessContinuityPlanPossibleActions => _businessContinuityPlanPossibleActions;

    private List<BusinessContinuityPlanRecoveryCriteria> _businessContinuityPlanRecoveryCriterias = new();
    public ICollection<BusinessContinuityPlanRecoveryCriteria> BusinessContinuityPlanRecoveryCriterias => _businessContinuityPlanRecoveryCriterias;

    private List<BusinessContinuityPlanRecoveryOption> _businessContinuityPlanRecoveryOptions = new();
    public ICollection<BusinessContinuityPlanRecoveryOption> BusinessContinuityPlanRecoveryOptions => _businessContinuityPlanRecoveryOptions;

    private List<BusinessContinuityPlanVersioning> _businessContinuityPlanVersioning = new();
    public ICollection<BusinessContinuityPlanVersioning> BusinessContinuityPlanVersionings => _businessContinuityPlanVersioning;





}
