using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Contracts
{
    public interface IFinancialActionTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long Id);
    }
}
