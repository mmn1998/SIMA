using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskCriterias
{
    public class RiskCriteriaDomainService : IRiskCriteriaDomainService
    {
        private readonly SIMADBContext _context;

        public RiskCriteriaDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskCriterias.AnyAsync(b => b.Code == code && b.Id != new RiskCriteriaId(id));
            else
                return !await _context.RiskCriterias.AnyAsync(b => b.Code == code);
        }

    }
}
