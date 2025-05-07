using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskLevels
{
    public class RiskLevelDomainService : IRiskLevelDomainService
    {
        private readonly SIMADBContext _context;

        public RiskLevelDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskLevels.AnyAsync(b => b.Code == code && b.Id != new RiskLevelId(id));
            else
                return !await _context.RiskLevels.AnyAsync(b => b.Code == code);
        }
    }
}
