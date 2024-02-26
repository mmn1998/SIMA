using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class CreateWorkFlowActorUserCommand : ICommand<Result<long>>
    {
        public long WorkFlowActorId { get; set; }
        public List<long> UserId { get; set; }
    }
}
