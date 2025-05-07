using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.RejectionReason
{
    public class DeleteRejectionReasonCommand : ICommand<Result<long>>
    {
        public long StepId { get; set; }
        public long Id { get; set; }
    }
}
