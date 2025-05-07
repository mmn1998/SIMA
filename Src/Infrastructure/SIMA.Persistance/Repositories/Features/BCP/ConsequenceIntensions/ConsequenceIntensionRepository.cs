using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.ConsequenceIntensions;

public class ConsequenceIntensionRepository : Repository<ConsequenceIntension>, IConsequenceIntensionRepository
{
    private readonly SIMADBContext _context;

    public ConsequenceIntensionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConsequenceIntension> GetById(ConsequenceIntensionId Id)
    {
        var entity = await _context.ConsequenceIntensions.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}