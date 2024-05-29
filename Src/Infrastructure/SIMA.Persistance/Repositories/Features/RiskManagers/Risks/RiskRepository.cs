using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagers.Risks
{
    public class RiskRepository : Repository<Risk>, IRiskRepository
    {
        private readonly SIMADBContext _context;

        public RiskRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
    }
}
