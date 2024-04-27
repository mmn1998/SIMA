using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.SecurityCommitees.Meeting
{
    public class MeetingRepository : Repository<Domain.Models.Features.SecurityCommitees.Meetings.Entities.Meeting>, IMeetingRepository
    {
        private readonly SIMADBContext _context;

        public MeetingRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public Task<Domain.Models.Features.SecurityCommitees.Meetings.Entities.Meeting> GetById(long Id)
        {
            throw new NotImplementedException();
        }
    }
}
