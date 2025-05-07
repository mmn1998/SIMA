using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.ReferalLetters;

public class ReferalLetterDomaniService : IReferalLetterDomainService
{
    private readonly SIMADBContext _context;

    public ReferalLetterDomaniService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<string?> GetLastLetterNumber()
    {
        var entity = await _context.ReferralLetters.OrderByDescending(c => c.CreatedAt).FirstOrDefaultAsync();
        return entity?.LetterNumber;
    }

    public async Task<string?> GetBrokerTypeCodeByBrokerId(BrokerId brokerId)
    {
        var broker = await _context.Brokers.Include(x => x.BrokerType).FirstOrDefaultAsync(x => x.Id == brokerId);
        return broker?.BrokerType?.Code;
    }
}
