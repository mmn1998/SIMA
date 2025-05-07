using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

public class CurrentOccurrenceProbabilityValueRepository : Repository<CurrentOccurrenceProbabilityValue>, ICurrentOccurrenceProbabilityValueRepository
{
    private readonly SIMADBContext _context;

    public CurrentOccurrenceProbabilityValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<CurrentOccurrenceProbabilityValue> GetById(CurrentOccurrenceProbabilityValueId id)
    {
        return await _context.CurrentOccurrenceProbabilityValues.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}