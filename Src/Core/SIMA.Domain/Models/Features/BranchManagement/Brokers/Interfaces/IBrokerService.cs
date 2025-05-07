using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;

public interface IBrokerService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
