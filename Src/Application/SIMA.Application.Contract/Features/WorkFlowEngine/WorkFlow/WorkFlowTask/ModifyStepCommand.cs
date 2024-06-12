using Sima.Framework.Core.Mediator;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.Steps;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask
{
    public class ModifyStepCommand : ICommand<Result<long>>
    {

        public long Id { get; set; }
        public List<AddStepApprovalOption> StepApprovalOptions { get; set; }
        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        public long? FormId { get; set; }
    }
}
