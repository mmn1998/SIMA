using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.ThreatTypes
{
    public class ThreatTypeDomainService : IThreatTypeDomainService
    {
        private readonly SIMADBContext _context;

        public ThreatTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.ThreatTypes.AnyAsync(b => b.Code == code && b.Id != new ThreatTypeId(id));
            else
                return !await _context.ThreatTypes.AnyAsync(b => b.Code == code);
        }
    }
}
