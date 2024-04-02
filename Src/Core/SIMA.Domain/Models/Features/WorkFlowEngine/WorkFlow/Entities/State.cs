using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class State : Entity
{

    private State()
    {

    }
    private State(CreateStateArg arg)
    {
        Id = new StateId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
    }
    public async Task Modify(ModifyStateArgs arg, IWorkFlowDomainService service)
    {
        ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public static async Task<State> New(CreateStateArg arg, IWorkFlowDomainService service)
    {
        await CreateGuards(arg, service);
        return new State(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }

    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }

    public StateId Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public WorkFlowId? WorkFlowId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    private List<Step> _steps = new();
    public virtual ICollection<Step> Steps => _steps;
    private List<IssueChangeHistory> _issueChangeHistories = new();
    public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;
    private List<Issue> _issues = new();
    public ICollection<Issue> Issues => _issues;
    public virtual WorkFlow? WorkFlow { get; set; }
    public virtual List<IssueHistory> SourceIssueHistories { get; set; }
    public virtual List<IssueHistory>? TargetIssueHistories { get; set; }

    #region Gaurds

    private static async Task CreateGuards(CreateStateArg arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (await service.SteteIsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyStateArgs arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (await service.SteteIsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
}
