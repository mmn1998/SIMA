using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftOrigins;

public class DraftOriginRepository : Repository<DraftOrigin>, IDraftOriginRepository
{
    private readonly SIMADBContext _context;

    public DraftOriginRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftOrigin> GetByCode(string code)
    {
        var entity = await _context.DraftOrigins.FirstOrDefaultAsync(x => x.Code == code);
        return entity ;
    }

    public async Task<DraftOrigin> GetById(DraftOriginId Id)
    {
        var entity = await _context.DraftOrigins.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}