using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities
{
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
            ModifiedBy = arg.CreatedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }

        public void ChangeStatus(ChangeStatusArg arg)
        {
            StateId = new StateId(arg.StateId);
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

        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public void Activate()
        {
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
        public long? ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; private set; }
        public Step Source { get; private set; }
        public Step Target { get; private set; }
        public WorkFlow.Entities.WorkFlow  WorkFlow { get; private set; }

        public StateId? StateId { get; private set; }
        public virtual State? State { get; private set; }

    }
}
