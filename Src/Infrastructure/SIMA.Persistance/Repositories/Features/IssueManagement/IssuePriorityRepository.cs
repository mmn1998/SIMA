using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.IssueManagement;

public class IssuePriorityRepository : Repository<IssuePriority>, IIssuePriorityRepository
{
    private readonly SIMADBContext _context;

    public IssuePriorityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IssuePriority> GetById(long id)
    {
        var result = await _context.IssuePriorities.FirstOrDefaultAsync(ip => ip.Id == new IssuePriorityId(id));
        result.NullCheck();
        return result;
    }
}
