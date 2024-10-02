using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor;

public interface IWorkFlowActorQueryRepository : IQueryRepository
{
    Task<GetWorkFlowActorQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetWorkFlowActorQueryResult>>> GetAll(GetAllWorkFlowActorsQuery request);
    Task<bool> CheckAccessToIsEveryOne(long workFlowActorId);
    Task<IEnumerable<GetWorkflowActorEmployeeQueryResult>> GetEmployee(long actorId);
}
