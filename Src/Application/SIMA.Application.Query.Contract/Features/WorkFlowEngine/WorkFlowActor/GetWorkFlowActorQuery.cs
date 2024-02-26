using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class GetWorkFlowActorQuery : IQuery<Result<GetWorkFlowActorQueryResult>>
    {
        public long Id { get; set; }
    }
}
