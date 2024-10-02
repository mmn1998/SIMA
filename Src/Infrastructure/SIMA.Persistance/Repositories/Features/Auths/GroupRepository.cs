using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Groups;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly SIMADBContext _context;

        public GroupRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Group> GetById(long id)
        {
            var entity = await _context.Groups
            .Include(g => g.UserGroups)
            .Include(g => g.GroupPermissions)
            .Include(g => g.FormGroups)
                .ThenInclude(g => g.Form)
                    .ThenInclude(x => x.FormPermissions)
                    .FirstOrDefaultAsync(g => g.Id == new GroupId(id));
            entity.NullCheck();
            return entity;
        }
    }
}
