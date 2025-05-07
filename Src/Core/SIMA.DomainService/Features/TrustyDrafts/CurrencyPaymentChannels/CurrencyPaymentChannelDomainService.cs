using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.CurrencyPaymentChannels;

public class CurrencyPaymentChannelDomainService : ICurrencyPaymentChannelDomainService
{
    private readonly SIMADBContext _context;

    public CurrencyPaymentChannelDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, CurrencyPaymentChannelId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.CurrencyPaymentChannels.AnyAsync(x => x.Code == code);
        else result = !await _context.CurrencyPaymentChannels.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}