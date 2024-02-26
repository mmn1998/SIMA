using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.WorkFlowActorRepository
{
    public class WorkFlowActorRepository : Repository<WorkFlowActor>, IWorkFlowActorRepository
    {
        private readonly SIMADBContext _context;
        public WorkFlowActorRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<WorkFlowActor> GetById(long id)
        {
            WorkFlowActor actor = null;

            actor = await _context.WorkFlowActors.Include(x => x.WorkFlowActorRoles).Include(x => x.WorkFlowActorUsers).Include(x => x.WorkFlowActorGroups).FirstOrDefaultAsync(x => x.Id == new WorkFlowActorId(id));

            if (actor is null)
                actor = await _context.WorkFlowActors.FirstOrDefaultAsync(x => x.Id == new WorkFlowActorId(id));

            return actor;

        }
    }
}
