using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class MeetingHoldingStatusDomainService : IMeetingHoldingStatusDomainService
    {
        private readonly SIMADBContext _context;

        public MeetingHoldingStatusDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string Code, long Id)
        {
            bool result = false;
            if (Id > 0)
                result = await _context.MeetingHoldingStatuses.AnyAsync(b => b.Code == Code && b.Id != new MeetingHoldingStatusId(Id));
            else
                result = await _context.MeetingHoldingStatuses.AnyAsync(b => b.Code == Code);
            return result;
        }
    }
}
