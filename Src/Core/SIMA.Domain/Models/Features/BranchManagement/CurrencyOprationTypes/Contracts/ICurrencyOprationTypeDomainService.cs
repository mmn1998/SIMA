using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Contracts
{
    public interface ICurrencyOprationTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long Id);
    }
}
