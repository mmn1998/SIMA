using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.SeverityValues;

public class SeverityValueRepository : Repository<SeverityValue>, ISeverityValueRepository
{
    private readonly SIMADBContext _context;

    public SeverityValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<SeverityValue> GetById(SeverityValueId id)
    {
        return await _context.SeverityValues.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}