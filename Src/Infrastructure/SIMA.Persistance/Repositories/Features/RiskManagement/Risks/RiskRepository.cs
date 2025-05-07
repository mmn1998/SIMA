using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.Risks;

public class RiskRepository : Repository<Risk>, IRiskRepository
{
    private readonly SIMADBContext _context;

    public RiskRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Risk> GetById(RiskId id)
    {
        return await _context.Risks
            .Include(x => x.PreventiveActions)
            .Include(x => x.CorrectiveActions)
            .Include(x => x.EffectedAssets)
                .ThenInclude(x => x.Vulnerabilities)
            .Include(x => x.ServiceRisks)
                .ThenInclude(x => x.ServiceRiskImpacts)
            .Include(x => x.Threats)
            .Include(x => x.RiskRelatedIssues)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw SimaResultException.NotFound;

    }

    public async Task<Risk?> GetLast()
    {
        return await _context.Risks.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
    }
}
