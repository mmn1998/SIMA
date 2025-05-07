using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.ConsequenceCategories;

public class ConsequenceCategoryRepository : Repository<ConsequenceCategory>, IConsequenceCategoryRepository
{
    private readonly SIMADBContext _context;

    public ConsequenceCategoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<ConsequenceCategory> GetById(ConsequenceCategoryId id)
    {
        return await _context.ConsequenceCategories.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}