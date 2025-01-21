using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Scenarios.Contracts;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.Scenarios
{
    public class ScenarioRepository : Repository<Scenario>, IScenarioRepisitory
    {
        private readonly SIMADBContext _context;

        public ScenarioRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Scenario> GetById(ScenarioId Id)
        {
            var entity = await _context.Scenarios.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
