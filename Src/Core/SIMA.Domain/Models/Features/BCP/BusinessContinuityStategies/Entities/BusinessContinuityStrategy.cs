using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Events;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Args;
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
        Id = new(arg.Id);
        StrategyTypeId = new(arg.StrategyTypeId);
        Title = arg.Title;
        Code = arg.Code;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        ReviewDate = arg.ReviewDate;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        AddDomainEvent(new CreateBusinessContinuityStrategyEvent
            (issueId: arg.IssueId, mainAggregateType: MainAggregateEnums.BusinessContinuityStrategy,
            name: arg.Title, sourceId: arg.Id, issuePriorityId: 0, issueWeightCategoryId: 0));
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
    #region Properties
    public BusinessContinuityStrategyId Id { get; private set; }
    public StrategyTypeId StrategyTypeId { get; private set; }
    public virtual StrategyType StrategyType { get; private set; }
    public string? Code { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public DateTime? ReviewDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRelatedEntities
        DeleteBusinessContinuityStrategyDocuments(userId);
        DeleteBusinessContinuityStrategyObjectives(userId);
        DeleteBusinessContinuityStrategySolutions(userId);
        DeleteBusinessContinuityStrategyResponsibles(userId);
        DeleteBusinessContinuityStrategyRelatedIssues(userId);
        #endregion
    }
    #region AddMethods
    public async Task AddBusinessContinuityStrategyObjectives(List<CreateBusinessContinuityStrategyObjectiveArg> args, IBusinessContinuityStategyDomainService domainService)
    {
        foreach (var arg in args)
        {
            var entity = await BusinessContinuityStrategyObjective.Create(arg, domainService);
            _businessContinuityStrategyObjectives.Add(entity);
        }
    }
    public async Task AddBusinessContinuityStrategySolutions(List<CreateBusinessContinuityStratgySolutionArg> args, IBusinessContinuityStategyDomainService domainService)
    {
        foreach (var arg in args)
        {
            var entity = await BusinessContinuityStratgySolution.Create(arg, domainService);
            _businessContinuityStratgySolution.Add(entity);
        }
    }
    public void AddBusinessContinuityStrategyDocuments(List<CreateBusinessContinuityStrategyDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BusinessContinuityStrategyDocument.Create(arg);
            _businessContinuityStrategyDocuments.Add(entity);
        }
    }
    public void AddBusinessContinuityStrategyResponsibles(List<CreateBusinessContinuityStratgyResponsibleArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BusinessContinuityStratgyResponsible.Create(arg);
            _businessContinuityStratgyResponsible.Add(entity);
        }
    }
    public void AddBusinessContinuityStrategyRelatedIssues(CreateBusinessContinuityStrategyIssueArg arg)
    {
        var entity = BusinessContinuityStrategyIssue.Create(arg);
        _businessContinuityStrategyIssues.Add(entity);
    }
    #endregion
    #region ModifyMethods
    public async Task ModifyBusinessContinuityStrategyObjectives(List<CreateBusinessContinuityStrategyObjectiveArg> args, IBusinessContinuityStategyDomainService domainService)
    {
        var activeEntities = _businessContinuityStrategyObjectives.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Title == x.Title));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Title == x.Title));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessContinuityStrategyObjectives.FirstOrDefault(x => x.Title == arg.Title && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = await BusinessContinuityStrategyObjective.Create(arg, domainService);
                _businessContinuityStrategyObjectives.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public async Task ModifyBusinessContinuityStrategySolutions(List<CreateBusinessContinuityStratgySolutionArg> args, IBusinessContinuityStategyDomainService domainService)
    {
        var activeEntities = _businessContinuityStratgySolution.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Title == x.Title));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Title == x.Title));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessContinuityStratgySolution.FirstOrDefault(x => x.Title == arg.Title && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = await BusinessContinuityStratgySolution.Create(arg, domainService);
                _businessContinuityStratgySolution.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyBusinessContinuityStrategyDocuments(List<CreateBusinessContinuityStrategyDocumentArg> args)
    {
        var activeEntities = _businessContinuityStrategyDocuments.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessContinuityStrategyDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = BusinessContinuityStrategyDocument.Create(arg);
                _businessContinuityStrategyDocuments.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyBusinessContinuityStrategyResponsibles(List<CreateBusinessContinuityStratgyResponsibleArg> args)
    {
        var activeEntities = _businessContinuityStratgyResponsible.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.PlanResponsibilityId == x.PlanResponsibilityId.Value && c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.PlanResponsibilityId.Value == x.PlanResponsibilityId && c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessContinuityStratgyResponsible.FirstOrDefault(x => x.PlanResponsibilityId.Value == arg.PlanResponsibilityId && x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = BusinessContinuityStratgyResponsible.Create(arg);
                _businessContinuityStratgyResponsible.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteBusinessContinuityStrategyObjectives(long userId)
    {
        foreach (var entity in _businessContinuityStrategyObjectives)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessContinuityStrategySolutions(long userId)
    {
        foreach (var entity in _businessContinuityStratgySolution)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessContinuityStrategyDocuments(long userId)
    {
        foreach (var entity in _businessContinuityStrategyDocuments)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessContinuityStrategyResponsibles(long userId)
    {
        foreach (var entity in _businessContinuityStratgyResponsible)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessContinuityStrategyRelatedIssues(long userId)
    {
        foreach (var entity in _businessContinuityStrategyIssues)
        {
            AddDomainEvent(new DeleteBusinessContinuityStrategyEvent
                (issueId: entity.IssueId.Value));
            entity.Delete(userId);
        }
    }
    #endregion
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


    private List<BusinessContinuityStrategyIssue> _businessContinuityStrategyIssues = new();
    public ICollection<BusinessContinuityStrategyIssue> BusinessContinuityStrategyIssues => _businessContinuityStrategyIssues;


    private List<BusinessContinuityStratgySolution> _businessContinuityStratgySolution = new();
    public ICollection<BusinessContinuityStratgySolution> BusinessContinuityStratgySolutions => _businessContinuityStratgySolution;

}
