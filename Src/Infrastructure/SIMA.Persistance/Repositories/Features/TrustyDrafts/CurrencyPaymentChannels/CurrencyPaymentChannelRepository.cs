using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.CurrencyPaymentChannels;

public class CurrencyPaymentChannelRepository : Repository<CurrencyPaymentChannel>, ICurrencyPaymentChannelRepository
{
    private readonly SIMADBContext _context;

    public CurrencyPaymentChannelRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CurrencyPaymentChannel> GetById(CurrencyPaymentChannelId Id)
    {
        var entity = await _context.CurrencyPaymentChannels.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}