using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskTypes
{
    public class RiskTypeRepository : Repository<RiskType>, IRiskTypeRepository
    {
        private readonly SIMADBContext _context;
        public RiskTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RiskType> GetById(long id)
        {
            var result = await _context.RiskTypes.FirstOrDefaultAsync(ip => ip.Id == new RiskTypeId(id));
            result.NullCheck();
            return result;
        }
    }
}
