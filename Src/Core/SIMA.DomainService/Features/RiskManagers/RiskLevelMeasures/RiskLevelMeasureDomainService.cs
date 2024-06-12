using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskLevelMeasures
{
    public class RiskLevelMeasureDomainService : IRiskLevelMeasureDomainService
    {
        private readonly SIMADBContext _context;

        public RiskLevelMeasureDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskLevelMeasures.AnyAsync(b => b.Code == code && b.Id != new RiskLevelMeasureId(id));
            else
                return !await _context.RiskLevelMeasures.AnyAsync(b => b.Code == code);
        }
    }
}
