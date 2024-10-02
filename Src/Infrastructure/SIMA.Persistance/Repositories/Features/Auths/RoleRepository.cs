using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    private readonly SIMADBContext _context;

    public RoleRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Role> GetById(long id)
    {
        var entity = await _context.Roles
        .Include(r => r.RolePermissions)
        .Include(r=>r.UserRoles)
        .Include(r=>r.FormRoles)
            .ThenInclude(r=>r.Form)
                .ThenInclude(r=>r.FormPermissions)
            .FirstOrDefaultAsync(r => r.Id == new RoleId(id));
        entity.NullCheck();
        return entity;
    }
}
