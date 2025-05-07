using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces
{
    public interface ICurrencyTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long Id);
        Task<bool> IsBaseCurrencyUnique();
        Task<CurrencyType> IsBaseCurrency();
    }
}
