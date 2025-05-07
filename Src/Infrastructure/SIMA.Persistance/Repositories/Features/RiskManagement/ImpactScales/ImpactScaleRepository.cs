using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.ImpactScales
{
    public class ImpactScaleRepository : Repository<ImpactScale>, IImpactScaleRepository
    {
        private readonly SIMADBContext _context;
        public ImpactScaleRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ImpactScale> GetById(long id)
        {
            var result = await _context.ImpactScales.FirstOrDefaultAsync(ip => ip.Id == new ImpactScaleId(id));
            result.NullCheck();
            return result;
        }
    }
}
