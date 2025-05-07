using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.RejectionReason
{
    public class CreateRejectionReasonCommand : ICommand<Result<long>>
    {
        public long StepId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
