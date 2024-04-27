using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Interfaces;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.SecurityCommitees.Subjects
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly SIMADBContext _context;

        public SubjectRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public Task<Subject> GetById(long Id)
        {
            throw new NotImplementedException();
        }
    }
}
