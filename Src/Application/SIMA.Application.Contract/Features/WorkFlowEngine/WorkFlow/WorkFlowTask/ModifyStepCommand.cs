using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask
{
    public class ModifyStepCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        //public List<long> ActorId { get; set; }
        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        //public long? ActionTypeId { get; set; }
        //public long? MainEntityId { get; set; }
        public long? StateId { get; set; }
        //public string? BpmnId { get; set; }
    }
}
