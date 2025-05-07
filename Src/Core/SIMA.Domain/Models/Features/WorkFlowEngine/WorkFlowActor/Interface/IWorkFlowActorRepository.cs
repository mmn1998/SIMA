using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface
{
    public interface IWorkFlowActorRepository : IRepository<Entites.WorkFlowActor>
    {
        Task<Entites.WorkFlowActor> GetById(long id);
        Task<Entites.WorkFlowActor> GetWorkFlowActorByUser(long workFlowId);
    }
}
