using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Roles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Roles;

public class RoleService : IRoleService
{
    private readonly SIMADBContext _context;

    public RoleService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsRoleSatisfied(string code, string englishKey, long id)
    {
        bool result = false;
        if (id > 0)
            result = await _context.Roles.AnyAsync(b => b.Code == code && b.EnglishKey == englishKey && b.Id != new RoleId(id));
        else
            result = await _context.Roles.AnyAsync(b => b.Code == code && b.EnglishKey == englishKey);
        return result;
    }
}
