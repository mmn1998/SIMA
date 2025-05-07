using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.Severities;

public class SeverityRepository : Repository<Severity>, ISeverityRepository
{
    private readonly SIMADBContext _context;

    public SeverityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Severity> GetById(SeverityId id)
    {
        return await _context.Severities.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}