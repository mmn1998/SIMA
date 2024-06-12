using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskPossibilities
{
    public class RiskPossibilityDomainService : IRiskPossibilityDomainService
    {
        private readonly SIMADBContext _context;

        public RiskPossibilityDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskPossibilities.AnyAsync(b => b.Code == code && b.Id != new RiskPossibilityId(id));
            else
                return !await _context.RiskPossibilities.AnyAsync(b => b.Code == code);
        }
    }
}
