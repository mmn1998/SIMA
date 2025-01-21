using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.AgentBankWageShareStatuses;

public class AgentBankWageShareStatusRepository : Repository<AgentBankWageShareStatus>, IAgentBankWageShareStatusRepository
{
    private readonly SIMADBContext _context;

    public AgentBankWageShareStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AgentBankWageShareStatus> GetById(AgentBankWageShareStatusId Id)
    {
        var entity = await _context.AgentBankWageShareStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}
