using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.WorkFlowEngine.Projects
{
    public class ProjectDomainService : IProjectDomainService
    {
        private readonly SIMADBContext _context;

        public ProjectDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return await _context.Projects.AnyAsync(b => b.Code == code && b.Id != new ProjectId(id));
            else
            {
                var result = await _context.Projects.AnyAsync(b => b.Code == code);
                return result;
            }
        }
    }
}
