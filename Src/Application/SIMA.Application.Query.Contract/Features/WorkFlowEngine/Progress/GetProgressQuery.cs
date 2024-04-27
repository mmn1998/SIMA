using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress
{
    public class GetProgressQuery : IQuery<Result<GetProgressQueryResult>>
    {
        public long Id { get; set; }
    }
}
