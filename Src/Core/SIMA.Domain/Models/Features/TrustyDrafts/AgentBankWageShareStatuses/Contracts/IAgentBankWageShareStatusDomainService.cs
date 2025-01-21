using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Contracts;

public interface IAgentBankWageShareStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AgentBankWageShareStatusId? id = null);
}
