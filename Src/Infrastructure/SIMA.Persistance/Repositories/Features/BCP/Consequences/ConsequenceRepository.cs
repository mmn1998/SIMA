using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Consequences.Contracts;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.Consequences;

public class ConsequenceRepository : Repository<Consequence>, IConsequenceRepository
{
    private readonly SIMADBContext _context;

    public ConsequenceRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Consequence> GetById(ConsequenceId Id)
    {
        var entity = await _context.Consequences.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}