using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Contracts;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.HappeningPossiblities;

public class HappeningPossibilityRepository : Repository<HappeningPossibility>, IHappeningPossibilityRepository
{
    private readonly SIMADBContext _context;

    public HappeningPossibilityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<HappeningPossibility> GetById(HappeningPossibilityId Id)
    {
        var entity = await _context.HappeningPossibilities.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}
