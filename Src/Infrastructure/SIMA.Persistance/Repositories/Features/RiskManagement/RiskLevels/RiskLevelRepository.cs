using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskLevels
{
    public class RiskLevelRepository : Repository<RiskLevel>, IRiskLevelRepository
    {
        private readonly SIMADBContext _context;
        public RiskLevelRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RiskLevel> GetById(long id)
        {
            var result = await _context.RiskLevels.FirstOrDefaultAsync(ip => ip.Id == new RiskLevelId(id));
            result.NullCheck();
            return result;
        }
    }
}
