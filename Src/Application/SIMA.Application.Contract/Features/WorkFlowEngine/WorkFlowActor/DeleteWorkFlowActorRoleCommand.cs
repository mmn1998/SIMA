using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class DeleteWorkFlowActorRoleCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long WorkFlowActorId { get; set; }
    }
}
