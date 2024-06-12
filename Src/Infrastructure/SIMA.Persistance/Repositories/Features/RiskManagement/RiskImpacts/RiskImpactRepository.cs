using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskImpacts
{
    public class RiskImpactRepository : Repository<RiskImpact>, IRiskImpactRepository
    {
        private readonly SIMADBContext _context;
        public RiskImpactRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RiskImpact> GetById(long id)
        {
            var result = await _context.RiskImpacts.FirstOrDefaultAsync(ip => ip.Id == new RiskImpactId(id));
            result.NullCheck();
            return result;
        }
    }
}
