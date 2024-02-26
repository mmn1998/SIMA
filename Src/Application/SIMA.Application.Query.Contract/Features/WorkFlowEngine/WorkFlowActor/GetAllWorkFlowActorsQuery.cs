using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class GetAllWorkFlowActorsQuery : IQuery<Result<List<GetWorkFlowActorQueryResult>>>
    {
    }
}
