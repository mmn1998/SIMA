using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskDegrees
{
    public class RiskDegreeRepository : Repository<RiskDegree>, IRiskDegreeRepository
    {
        private readonly SIMADBContext _context;
        public RiskDegreeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RiskDegree> GetById(long id)
        {
            var result = await _context.RiskDegrees.FirstOrDefaultAsync(ip => ip.Id == new RiskDegreeId(id));
            result.NullCheck();
            return result;
        }
    }
}
