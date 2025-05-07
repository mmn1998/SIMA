using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.ImpactScales
{
    public class ImpactScaleDomainService : IImpactScaleDomainService
    {
        private readonly SIMADBContext _context;

        public ImpactScaleDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.ImpactScales.AnyAsync(b => b.Code == code && b.Id != new ImpactScaleId(id));
            else
                return !await _context.ImpactScales.AnyAsync(b => b.Code == code);
        }
    }
}
