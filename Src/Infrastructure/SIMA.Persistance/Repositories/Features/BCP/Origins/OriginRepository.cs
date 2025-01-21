using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Origins.Contracts;
using SIMA.Domain.Models.Features.BCP.Origins.Entities;
using SIMA.Domain.Models.Features.BCP.Origins.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.Origins;

public class OriginRepository : Repository<Origin>, IOriginRepository
{
    private readonly SIMADBContext _context;

    public OriginRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Origin> GetById(OriginId Id)
    {
        var entity = await _context.Origins.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}