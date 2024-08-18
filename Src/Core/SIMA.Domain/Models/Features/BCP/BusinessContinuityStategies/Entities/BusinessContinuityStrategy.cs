using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Entities;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategy : Entity, IAggregateRoot
{
    private BusinessContinuityStrategy() { }
    private BusinessContinuityStrategy(CreateBusinessContinuityStategyArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        StrategyTypeId = new(arg.StrategyTypeId);
        Title = arg.Title;
        Code = arg.Code;
        Description = arg.Description;
        CordinatorId = arg.CordinatorId;
        IsStableStrategy = arg.IsStableStrategy;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        ReviewDate = arg.ReviewDate;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BusinessContinuityStrategy> Create(CreateBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessContinuityStrategy(arg);
    }
    public async Task Modify(ModifyBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {
        await ModifyGuards(arg, service);
        Title = arg.Title;
        Code = arg.Code;
        StrategyTypeId = new(arg.StrategyTypeId);
        Description = arg.Description;
        CordinatorId = arg.CordinatorId;
        IsStableStrategy = arg.IsStableStrategy;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        ReviewDate = arg.ReviewDate;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {

    }
    #endregion
    public BusinessContinuityStrategyId Id { get; private set; }
    public StrategyTypeId StrategyTypeId { get; private set; }
    public virtual StrategyType StrategyType { get; private set; }
    public string Code { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string IsStableStrategy { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public DateTime? ReviewDate { get; private set; }
    public long CordinatorId { get; private set; }
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
    private List<BusinessContinuityStrategyObjective> _businessContinuityStrategyObjectives = new();
    public ICollection<BusinessContinuityStrategyObjective> BusinessContinuityStrategyObjectives => _businessContinuityStrategyObjectives;
    private List<BusinessContinuityStrategyStaff> _businessContinuityStrategyStaff = new();
    public ICollection<BusinessContinuityStrategyStaff> BusinessContinuityStrategyStaff => _businessContinuityStrategyStaff;
    private List<BusinessContinuityStrategyRisk> _businessContinuityStrategyRisks = new();
    public ICollection<BusinessContinuityStrategyRisk> BusinessContinuityStrategyRisks => _businessContinuityStrategyRisks;
    private List<BusinessContinuityStrategyDocument> _businessContinuityStrategyDocuments = new();
    public ICollection<BusinessContinuityStrategyDocument> BusinessContinuityStrategyDocuments => _businessContinuityStrategyDocuments;
    private List<BusinessContinuityStrategyService> _businessContinuityStrategyServices = new();
    public ICollection<BusinessContinuityStrategyService> BusinessContinuityStrategyServices => _businessContinuityStrategyServices;


    private List<BusinessContinuityPlan> _businessContinuityPlans = new();
    public ICollection<BusinessContinuityPlan> BusinessContinuityPlans => _businessContinuityPlans;

    private List<BusinessContinuityStratgyResponsible> _businessContinuityStratgyResponsible = new();
    public ICollection<BusinessContinuityStratgyResponsible> BusinessContinuityStratgyResponsibles => _businessContinuityStratgyResponsible;


    private List<BusinessContinuityPlanStratgy> _businessContinuityPlanStratgy = new();
    public ICollection<BusinessContinuityPlanStratgy> BusinessContinuityPlanStratgies => _businessContinuityPlanStratgy;


    private List<BusinessContinuityStratgySolution> _businessContinuityStratgySolution = new();
    public ICollection<BusinessContinuityStratgySolution> BusinessContinuityStratgySolutions => _businessContinuityStratgySolution;

}
