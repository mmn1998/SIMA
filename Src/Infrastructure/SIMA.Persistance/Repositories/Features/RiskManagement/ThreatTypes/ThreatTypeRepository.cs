using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.ThreatTypes
{
    internal class ThreatTypeRepository : Repository<ThreatType>, IThreatTypeRepository
    {
        private readonly SIMADBContext _context;
        public ThreatTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ThreatType> GetById(long id)
        {
            var result = await _context.ThreatTypes.FirstOrDefaultAsync(ip => ip.Id == new ThreatTypeId(id));
            result.NullCheck();
            return result;
        }
    }
}
