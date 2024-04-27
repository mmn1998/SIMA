using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.SecurityCommitees.MeetingHoldingReasons;

public class MeetingHoldingReasonDomainService : IMeetingHoldingReasonDomainService
{
    private readonly SIMADBContext _context;

    public MeetingHoldingReasonDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string Code, long Id)
    {
        bool result = false;
        if (Id > 0)
            result = !await _context.MeetingHoldingReasons.AnyAsync(b => b.Code == Code && b.Id != new MeetingHoldingReasonId(Id));
        else
            result = !await _context.MeetingHoldingReasons.AnyAsync(b => b.Code == Code);
        return result;
    }
}
