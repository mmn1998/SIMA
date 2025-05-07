using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftCurrencyOrigins;

public class DraftCurrencyOriginRepository : Repository<DraftCurrencyOrigin>, IDraftCurrencyOriginRepository
{
    private readonly SIMADBContext _context;

    public DraftCurrencyOriginRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftCurrencyOrigin> GetById(DraftCurrencyOriginId Id)
    {
        var entity = await _context.DraftCurrencyOrigins.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}