using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.IssueManagement
{
    public class IssueLinkReasonRepository : Repository<IssueLinkReason>, IIssueLinkReasonRepository
    {
        private readonly SIMADBContext _context;

        public IssueLinkReasonRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }



        public async Task<IssueLinkReason> GetById(long id)
        {
            var enitiy = await _context.IssueLinkReasons.FirstOrDefaultAsync(x => x.Id == new IssueLinkReasonId(id));
            enitiy.NullCheck();
            return enitiy;
        }
    }
}
