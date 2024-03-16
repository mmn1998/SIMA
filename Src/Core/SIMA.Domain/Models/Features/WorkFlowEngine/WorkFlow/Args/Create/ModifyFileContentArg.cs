using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class ModifyFileContentArg
    {
        public string? BpmnId { get; set; }
        public List<WorkFlowActorArg> WorkFlowActors { get; set; }
        public List<ProgressArg> Progresses { get; set; }
        public List<StepArg> Steps { get; set; }
        public string? FileContent { get; set; }
        public DateTime? ModifyAt { get; set; }
        public long? ModifyBy { get; set; }


    }
}
