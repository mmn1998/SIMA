using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.IssueManagement
{
    public class IssueTypeRepository : Repository<IssueType>, IIssueTypeRepository
    {
        private readonly SIMADBContext _context;
        public IssueTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IssueType> GetById(long id)
        {
            var result = await _context.IssueTypes.FirstOrDefaultAsync(ip => ip.Id == new IssueTypeId(id));
            result.NullCheck();
            return result;
        }
    }
}
