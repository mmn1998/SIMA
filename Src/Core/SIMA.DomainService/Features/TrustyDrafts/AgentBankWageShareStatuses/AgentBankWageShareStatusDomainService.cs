using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.AgentBankWageShareStatuses;

public class AgentBankWageShareStatusDomainService : IAgentBankWageShareStatusDomainService
{
    private readonly SIMADBContext _context;

    public AgentBankWageShareStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AgentBankWageShareStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.AgentBankWageShareStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.AgentBankWageShareStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}
