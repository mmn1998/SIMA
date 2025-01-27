using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.InherentOccurrenceProbabilityValues;

public class InherentOccurrenceProbabilityValueRepository : Repository<InherentOccurrenceProbabilityValue>, IInherentOccurrenceProbabilityValueRepository
{
    private readonly SIMADBContext _context;

    public InherentOccurrenceProbabilityValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<InherentOccurrenceProbabilityValue> GetById(InherentOccurrenceProbabilityValueId id)
    {
        return await _context.InherentOccurrenceProbabilityValues.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}