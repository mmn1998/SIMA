using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskImpacts
{
    public class RiskImpactDomainService : IRiskImpactDomainService
    {
        private readonly SIMADBContext _context;

        public RiskImpactDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskImpacts.AnyAsync(b => b.Code == code && b.Id != new RiskImpactId(id));
            else
                return !await _context.RiskImpacts.AnyAsync(b => b.Code == code);
        }
    }
}
