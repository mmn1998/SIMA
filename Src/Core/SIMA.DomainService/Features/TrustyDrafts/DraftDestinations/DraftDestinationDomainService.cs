using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftDestinations
{
    public class DraftDestinationDomainService : IDraftDestinationDomainService
    {
        private readonly SIMADBContext _context;

        public DraftDestinationDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, DraftDestinationId? Id = null)
        {
            bool result = false;
            if (Id == null) result = !await _context.DraftDestinations.AnyAsync(x => x.Code == code);
            else result = !await _context.DraftDestinations.AnyAsync(x => x.Code == code && x.Id != Id);
            return result;
        }
    }
}
