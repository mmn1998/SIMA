using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Contracts
{
    public interface IServiceTaskRepository : IRepository<ServiceTask>
    {
        Task<ServiceTask> GetById(long id);
    }
}
