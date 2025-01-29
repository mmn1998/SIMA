using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskValues;

public class RiskValueRepository : Repository<RiskValue>, IRiskValueRepository
{
    private readonly SIMADBContext _context;

    public RiskValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<RiskValue> GetById(RiskValueId id)
    {
        return await _context.RiskValues.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}