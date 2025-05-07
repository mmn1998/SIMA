using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Interface;

public interface IProgressRepository : IRepository<Entities.Progress>
{
    Task<Entities.Progress> GetById(long id);
}