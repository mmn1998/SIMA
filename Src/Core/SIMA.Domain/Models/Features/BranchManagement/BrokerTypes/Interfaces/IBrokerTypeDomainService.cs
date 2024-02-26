using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces
{
    public interface IBrokerTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long Id);
    }
}
