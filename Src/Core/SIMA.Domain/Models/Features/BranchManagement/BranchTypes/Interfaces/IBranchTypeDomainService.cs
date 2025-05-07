using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces
{
    public interface IBranchTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long Id);
    }
}
