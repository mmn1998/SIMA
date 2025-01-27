using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilities;

public class CurrentOccurrenceProbabilityRepository : Repository<CurrentOccurrenceProbability>, ICurrentOccurrenceProbabilityRepository
{
    private readonly SIMADBContext _context;

    public CurrentOccurrenceProbabilityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<CurrentOccurrenceProbability> GetById(CurrentOccurrenceProbabilityId id)
    {
        return await _context.CurrentOccurrenceProbabilities.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}