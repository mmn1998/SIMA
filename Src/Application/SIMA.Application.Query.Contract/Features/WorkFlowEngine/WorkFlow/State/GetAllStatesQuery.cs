using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class GetAllStatesQuery : IQuery<Result<List<GetStateQueryResult>>>
    {
    }
}
