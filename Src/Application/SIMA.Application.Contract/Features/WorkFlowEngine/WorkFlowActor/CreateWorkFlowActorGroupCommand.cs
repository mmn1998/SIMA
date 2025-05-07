using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class CreateWorkFlowActorGroupCommand : ICommand<Result<long>>
    {
        public long WorkFlowActorId { get; set; }
        public List<long> GroupId { get; set; }
    }
}
