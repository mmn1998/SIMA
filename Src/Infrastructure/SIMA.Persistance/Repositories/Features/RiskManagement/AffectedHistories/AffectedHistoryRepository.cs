using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.AffectedHistories;

public class AffectedHistoryRepository : Repository<AffectedHistory>, IAffectedHistoryRepository
{
    private readonly SIMADBContext _context;

    public AffectedHistoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<AffectedHistory> GetById(AffectedHistoryId id)
    {
        return await _context.AffectedHistories.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}