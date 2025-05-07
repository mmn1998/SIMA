using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskTypes
{
    public class RiskTypeDomainService : IRiskTypeDomainService
    {
        private readonly SIMADBContext _context;

        public RiskTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskTypes.AnyAsync(b => b.Code == code && b.Id != new RiskTypeId(id));
            else
                return !await _context.RiskTypes.AnyAsync(b => b.Code == code);
        }
    }
}
