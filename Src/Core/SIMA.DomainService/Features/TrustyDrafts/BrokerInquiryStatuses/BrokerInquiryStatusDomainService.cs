using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Contarcts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.BrokerInquiryStatuses;

public class BrokerInquiryStatusDomainService : IBrokerInquiryStatusDomainService
{
    private readonly SIMADBContext _context;

    public BrokerInquiryStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, BrokerInquiryStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.BrokerInquiryStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.BrokerInquiryStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}