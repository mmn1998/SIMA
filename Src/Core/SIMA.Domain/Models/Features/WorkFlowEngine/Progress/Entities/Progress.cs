using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;

public class Progress : Entity
{
    private Progress()
    {
    }
    private Progress(ProgressArg arg)
    {
        Id = new ProgressId(arg.Id);
        Name = arg.Name;
        Description = arg.Description;
        SourceId = new StepId(arg.SourceId);
        TargetId = arg.TargetId.HasValue ? new StepId(arg.TargetId.Value) : null;
        WorkFlowId = new WorkFlowId(arg.WorkFlowId);
        BpmnId = arg.BpmnId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static Progress New(ProgressArg arg)
    {
        return new Progress(arg);
    }
    public void Modify(ProgressArg arg)
    {
        Name = arg.Name;
        Description = arg.Description;
        SourceId = new StepId(arg.SourceId);
        TargetId = arg.TargetId.HasValue ? new StepId(arg.TargetId.Value) : null;
        BpmnId = arg.BpmnId;
        //  HasStoreProcedure = arg.HasStoreProcedure;
        ModifiedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public void SetStoreProcedures(List<ProgressStoreProcedureArg> progressStoreProcedureArgs)
    {
        foreach (var item in _progressStoreProcedures)
        {
            item.Delete();
        }
        var storeProcedures = progressStoreProcedureArgs.Select(ProgressStoreProcedure.Create);
        var storeProcedureIds = progressStoreProcedureArgs.Select(x => x.Id);
        var existSps = _progressStoreProcedures.Where(x => storeProcedureIds.Contains(x.Id.Value));
        var existIds = new List<long>();
        foreach (var item in existSps)
        {
            var addedSp = storeProcedures.FirstOrDefault(x => x.Id == item.Id);
            item.Modify(addedSp);
            item.Activate(addedSp.ProgressStoreProcedureParams.ToList());
            existIds.Add(item.Id.Value);
        }
        var notExistsSps = storeProcedures.Where(x => !existIds.Contains(x.Id.Value));
        _progressStoreProcedures.AddRange(notExistsSps);
    }
    public void ChangeStatus(ChangeStatusArg arg)
    {
        StateId = arg.StateId.HasValue ? new StateId(arg.StateId.Value) : null;
        ConditionExpression = arg.ConditionExpression;
        HasStoreProcedure = arg.ProgressStoreProcedures.Any() ? "1" : "0";
        SetStoreProcedures(arg.ProgressStoreProcedures);
    }
    public async Task Modify(ModifyProgressArg arg)
    {
        Name = arg.Name;
        Description = arg.Description;
        SourceId = new StepId(arg.SourceId);
        TargetId = arg.TargetId.HasValue ? new StepId(arg.TargetId.Value) : null;
        BpmnId = arg.BpmnId;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Activate(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (int)ActiveStatusEnum.Active;
    }

    public ProgressId Id { get; private set; }
    public StepId SourceId { get; set; }
    public StepId? TargetId { get; set; }
    public WorkFlowId WorkFlowId { get; set; }
    public string? Name { get; private set; }
    public string? BpmnId { get; private set; }
    public string? Description { get; private set; }
    public string? ConditionExpression { get; private set; }
    public string? HasStoreProcedure { get; private set; }
    public string? Extension { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; private set; }
    public Step Source { get; private set; }
    public Step Target { get; private set; }
    public WorkFlow.Entities.WorkFlow WorkFlow { get; private set; }
    private List<ProgressStoreProcedure> _progressStoreProcedures = new();
    public ICollection<ProgressStoreProcedure> ProgressStoreProcedures => _progressStoreProcedures;

    public StateId? StateId { get; private set; }
    public virtual State? State { get; private set; }

}
