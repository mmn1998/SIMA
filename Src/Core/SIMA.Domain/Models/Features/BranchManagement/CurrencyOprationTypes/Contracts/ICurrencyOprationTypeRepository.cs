using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Contracts
{
    public interface ICurrencyOprationTypeRepository : IRepository<CurrencyOprationType>
    {
        Task<CurrencyOprationType> GetById(long id);
    }
}
