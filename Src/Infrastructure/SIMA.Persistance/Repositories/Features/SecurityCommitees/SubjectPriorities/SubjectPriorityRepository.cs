using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.SecurityCommitees.SubjectPriorities;

public class SubjectPriorityRepository : Repository<SubjectPriority>, ISubjectPriorityRepository
{
    private readonly SIMADBContext _context;

    public SubjectPriorityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SubjectPriority> GetById(long Id)
    {
        var entity = await _context.SubjectPriorities.FirstOrDefaultAsync(sp => sp.Id == new SubjectPriorityId(Id));
        entity.NullCheck();

        return entity;
    }
}
