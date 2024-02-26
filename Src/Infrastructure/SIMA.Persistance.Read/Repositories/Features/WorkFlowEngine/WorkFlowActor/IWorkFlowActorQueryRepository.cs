using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor
{
    public interface IWorkFlowActorQueryRepository : IQueryRepository
    {
        Task<GetWorkFlowActorQueryResult> FindById(long id);
        Task<List<GetWorkFlowActorQueryResult>> GetAll();
    }
}
