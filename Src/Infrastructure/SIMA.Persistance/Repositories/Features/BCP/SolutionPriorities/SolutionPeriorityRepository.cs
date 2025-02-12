using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Entities;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.SolutionPeriorities;

public class SolutionPeriorityRepository : Repository<SolutionPriority>, ISolutionPriorityRepository
{
    private readonly SIMADBContext _context;

    public SolutionPeriorityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SolutionPriority> GetById(SolutionPriorityId Id)
    {
        var entity = await _context.SolutionPriorities.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}