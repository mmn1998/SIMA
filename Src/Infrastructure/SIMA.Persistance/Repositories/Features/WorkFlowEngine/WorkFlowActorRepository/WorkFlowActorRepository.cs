using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.WorkFlowActorRepository
{
    public class WorkFlowActorRepository : Repository<WorkFlowActor>, IWorkFlowActorRepository
    {
        private readonly SIMADBContext _context;
        private readonly ISimaIdentity _simaIdentity;

        public WorkFlowActorRepository(SIMADBContext context , ISimaIdentity simaIdentity) : base(context)
        {
            _context = context;
            _simaIdentity = simaIdentity;
        }
        public async Task<WorkFlowActor> GetById(long id)
        {
            WorkFlowActor actor = null;

            actor = await _context.WorkFlowActors.Include(x => x.WorkFlowActorRoles).Include(x => x.WorkFlowActorUsers).Include(x => x.WorkFlowActorGroups).FirstOrDefaultAsync(x => x.Id == new WorkFlowActorId(id));

            if (actor is null)
                actor = await _context.WorkFlowActors.FirstOrDefaultAsync(x => x.Id == new WorkFlowActorId(id));

            return actor;

        }

        public async Task<WorkFlowActor> GetWorkFlowActorByUser(long workFlowId)
        {
            var workFlowActor = await _context.WorkFlowActors.Where(x => x.WorkFlowId == new WorkFlowId(workFlowId))
                .Include(x => x.WorkFlowActorUsers)
                .Include(x => x.WorkFlowActorRoles)
                .Include(x => x.WorkFlowActorGroups).ToListAsync();

             var checkAccess = workFlowActor.Where(x =>
            x.WorkFlowActorUsers.Any(s => s.UserId == new UserId(_simaIdentity.UserId)) ||
            x.WorkFlowActorRoles.Any(s => _simaIdentity.RoleIds.Contains(s.RoleId.Value)) ||
            x.WorkFlowActorGroups.Any(s => _simaIdentity.GroupId.Contains(s.GroupId.Value))).FirstOrDefault();

            return checkAccess;            
        }
    }
}
