using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class MeetingHoldingStatusRepository :Repository<SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities.MeetingHoldingStatus>, IMeetingHoldingStatusRepository
    {
        private readonly SIMADBContext _context;

        public MeetingHoldingStatusRepository(SIMADBContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities.MeetingHoldingStatus> GetById(long Id)
        {
            var entity = await _context.MeetingHoldingStatuses.FirstOrDefaultAsync(sp => sp.Id == new Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.ValueObjects.MeetingHoldingStatusId(Id));
            entity.NullCheck();

            return entity;
        }
    }
}
