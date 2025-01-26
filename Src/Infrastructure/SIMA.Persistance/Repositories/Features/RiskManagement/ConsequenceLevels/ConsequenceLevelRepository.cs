using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.ConsequenceLevels;

public class ConsequenceLevelRepository : Repository<ConsequenceLevel>, IConsequenceLevelRepository
{
    private readonly SIMADBContext _context;

    public ConsequenceLevelRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<ConsequenceLevel> GetById(ConsequenceLevelId id)
    {
        return await _context.ConsequenceLevels
            .Include(x => x.RiskConsequences)
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}
