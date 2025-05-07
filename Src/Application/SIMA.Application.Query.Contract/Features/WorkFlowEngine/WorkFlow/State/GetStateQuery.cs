using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class GetStateQuery : IQuery<Result<GetStateQueryResult>>
    {
        public long Id { get; set; }
    }
}
