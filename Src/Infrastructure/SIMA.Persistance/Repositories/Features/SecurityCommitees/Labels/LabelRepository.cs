using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Interface;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.SecurityCommitees.Labels
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        private readonly SIMADBContext _context;

        public LabelRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public Task<Label> GetById(long Id)
        {
            throw new NotImplementedException();
        }
    }
}
