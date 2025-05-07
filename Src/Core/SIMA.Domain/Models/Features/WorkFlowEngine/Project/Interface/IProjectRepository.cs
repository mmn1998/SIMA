using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface
{
    public interface IProjectRepository : IRepository<Entites.Project>
    {
        Task<Entites.Project> GetById(long id);
        Task<List<Entites.ProjectGroup>> GetAllProjectGroup(long projectId);
        Task<ProjectGroup> GetProjectGroup(long id);
    }
}
