using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.InherentOccurrenceProbabilities;

public class InherentOccurrenceProbabilityRepository : Repository<InherentOccurrenceProbability>, IInherentOccurrenceProbabilityRepository
{
    private readonly SIMADBContext _context;

    public InherentOccurrenceProbabilityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<InherentOccurrenceProbability> GetById(InherentOccurrenceProbabilityId id)
    {
        return await _context.InherentOccurrenceProbabilities.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}