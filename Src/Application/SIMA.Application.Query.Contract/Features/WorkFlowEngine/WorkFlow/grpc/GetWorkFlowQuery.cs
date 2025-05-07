using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class GetWorkflowInfoByIdResponseQuery : IQuery<Result<GetWorkflowInfoByIdResponseQueryResult>>
{
    public long Id { get; set; }
}
