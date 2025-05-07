using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class GetWorkFlowActorQuery : IQuery<Result<GetWorkFlowActorQueryResult>>
    {
        public long Id { get; set; }
    }

    public class GetWorkflowActorEmployeeQuery : IQuery<Result<IEnumerable<GetWorkflowActorEmployeeQueryResult>>>
    {
        public long Id { get; set; }
    }
    public class GetWorkflowActorEmployeeQueryResult
    {
        public long EmployeeId { get; set; }
        public long ActorId { get; set; }
        public string Name { get; set; }
    }
}
