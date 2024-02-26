using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.IssueManagement.IssueLinkReasons
{
    public class IssueLinkReasonDomainService : IIssueLinkReasonDomainService
    {
        private readonly SIMADBContext _context;

        public IssueLinkReasonDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.IssueLinkReasons.AnyAsync(b => b.Code == code && b.Id != new IssueLinkReasonId(id));
            else
                return !await _context.IssueLinkReasons.AnyAsync(b => b.Code == code);
        }
    }
}
