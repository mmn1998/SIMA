using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskPossibilities
{
    public class RiskPossibilityRepository : Repository<RiskPossibility>, IRiskPossibilityRepository
    {
        private readonly SIMADBContext _context;
        public RiskPossibilityRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RiskPossibility> GetById(long id)
        {
            var result = await _context.RiskPossibilities.FirstOrDefaultAsync(ip => ip.Id == new RiskPossibilityId(id));
            result.NullCheck();
            return result;
        }
    }
}
