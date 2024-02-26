using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class DeleteStateCommand : ICommand<Result<long>>
    {
        public long WorkFlowId { get; set; }
        public long Id { get; set; }
    }
}
