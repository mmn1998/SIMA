using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Permissions.Interfaces;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Permissions;

public class PermissionService : IPermissionService
{
    private readonly SIMADBContext _context;

    public PermissionService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Permissions.AnyAsync(b => b.Code == code && b.Id != new PermissionId(id));
        else
            return !await _context.Permissions.AnyAsync(b => b.Code == code);
    }
}
