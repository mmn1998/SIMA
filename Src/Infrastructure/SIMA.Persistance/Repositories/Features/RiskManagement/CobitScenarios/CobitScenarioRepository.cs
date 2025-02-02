using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.CobitScenarios;

public class CobitScenarioRepository : Repository<CobitScenario>, ICobitScenarioRepository
{
    private readonly SIMADBContext _context;

    public CobitScenarioRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<CobitScenario> GetById(CobitScenarioId id)
    {
        return await _context.CobitScenarios.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}