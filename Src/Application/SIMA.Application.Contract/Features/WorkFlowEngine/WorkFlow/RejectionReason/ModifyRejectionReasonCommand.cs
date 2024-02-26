using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.RejectionReason
{
    public class ModifyRejectionReasonCommand: ICommand<Result<long>>
    {
        public int Id { get; set; }
        public int StepId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
