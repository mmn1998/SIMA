using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.ProjectRepository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly SIMADBContext _context;
        public ProjectRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProjectGroup>> GetAllProjectGroup(long projectId)
        {
            return await _context.Projects.SelectMany(x => x.ProjectGroups).Where(x => x.ProjectId == new ProjectId(projectId)).ToListAsync();
        }

        public async Task<ProjectGroup> GetProjectGroup(long id)
        {
            return await _context.Projects.SelectMany(x => x.ProjectGroups).Where(x => x.Id == new ProjectGroupId(id)).FirstOrDefaultAsync();
        }

        public async Task<Project> GetById(long id)
        {
            return await _context.Projects.Include(x => x.ProjectGroups).Include(x => x.ProjectMembers).FirstAsync(x => x.Id == new ProjectId(id));
        }

    }
}
