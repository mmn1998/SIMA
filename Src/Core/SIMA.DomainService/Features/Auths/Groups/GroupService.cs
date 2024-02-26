using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Groups.Interfaces;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Groups;

public class GroupService : IGroupService
{
    private readonly SIMADBContext _context;

    public GroupService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Groups.AnyAsync(b => b.Code == code && b.Id != new GroupId(id));
        else
            return !await _context.Groups.AnyAsync(b => b.Code == code);
    }
}
