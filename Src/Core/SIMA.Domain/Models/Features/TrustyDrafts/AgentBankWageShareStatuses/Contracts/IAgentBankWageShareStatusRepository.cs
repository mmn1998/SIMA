using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Contracts;

public interface IAgentBankWageShareStatusRepository : IRepository<AgentBankWageShareStatus>
{
    Task<AgentBankWageShareStatus> GetById(AgentBankWageShareStatusId id);
}
