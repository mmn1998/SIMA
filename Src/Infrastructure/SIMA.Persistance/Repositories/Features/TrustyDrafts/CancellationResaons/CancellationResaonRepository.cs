using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.CancellationResaons;

public class CancellationResaonRepository : Repository<CancellationResaon>, ICancellationResaonRepository
{
    private readonly SIMADBContext _context;

    public CancellationResaonRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CancellationResaon> GetById(CancellationResaonId Id)
    {
        var entity = await _context.CancellationResaons.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<CancellationResaon> GetByCode(string code)
    {
        var entity = await _context.CancellationResaons.FirstOrDefaultAsync(x => x.Code == code);
        return entity ;
    }
}