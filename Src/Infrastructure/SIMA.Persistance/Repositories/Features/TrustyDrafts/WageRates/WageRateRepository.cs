using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.WageRates;

public class WageRateRepository : Repository<WageRate>, IWageRateRepository
{
    private readonly SIMADBContext _context;

    public WageRateRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<WageRate> GetById(WageRateId Id)
    {
        var entity = await _context.WageRates.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}