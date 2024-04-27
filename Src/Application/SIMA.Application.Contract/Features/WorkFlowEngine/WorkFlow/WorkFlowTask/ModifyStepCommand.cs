using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask
{
    public class ModifyStepCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        public long? FormId { get; set; }
    }
}
