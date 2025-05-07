using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskLevelMeasures
{
    public class RiskLevelMeasureRepisitory : Repository<RiskLevelMeasure>, IRiskLevelMeasureRepository
    {
        private readonly SIMADBContext _context;
        public RiskLevelMeasureRepisitory(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RiskLevelMeasure> GetById(long id)
        {
            var result = await _context.RiskLevelMeasures.FirstOrDefaultAsync(ip => ip.Id == new RiskLevelMeasureId(id));
            result.NullCheck();
            return result;
        }
    }
}
