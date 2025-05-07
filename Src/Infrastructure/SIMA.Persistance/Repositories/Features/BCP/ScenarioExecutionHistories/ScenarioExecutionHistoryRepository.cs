using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Contracts;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.ScenarioExecutionHistories
{
    public class ScenarioExecutionHistoryRepository : Repository<ScenarioExecutionHistory>, IScenarioExecutionHistoryRepository
    {
        private readonly SIMADBContext _context;

        public ScenarioExecutionHistoryRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ScenarioExecutionHistory> GetById(ScenarioExecutionHistoryId Id)
        {
            var entity = await _context.ScenarioExecutionHistories.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
