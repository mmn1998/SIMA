using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class CreateWorkFlowArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ProjectId { get; set; }
        public string? BpmnId { get; set; }
        public long? ManagerRoleId { get; set; }
        public string? Description { get; set; }
        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long MainAggregateId { get; set; }
        public float? Ordering { get; set; }
        public string? FileContent { get; set; }
        public List<WorkFlowActorArg> WorkFlowActors { get; set; }
        public List<ProgressArg> Progresses { get; set; }
        public List<StepArg> Steps { get; set; }
    }
}
