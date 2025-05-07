using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskLevelCobits;

public class RiskLevelCobitRepository : Repository<RiskLevelCobit>, IRiskLevelCobitRepository
{
    private readonly SIMADBContext _context;

    public RiskLevelCobitRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<RiskLevelCobit> GetById(RiskLevelCobitId id)
    {
        return await _context.RiskLevelCobits.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}