using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.ScenarioHistories;

public class ScenarioHistoryRepository : Repository<ScenarioHistory>, IScenarioHistoryRepository
{
    private readonly SIMADBContext _context;

    public ScenarioHistoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<ScenarioHistory> GetById(ScenarioHistoryId id)
    {
        return await _context.ScenarioHistories.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}