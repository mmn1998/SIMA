using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;

public interface ICurrencyTypeRepository : IRepository<CurrencyType>
{
    Task<CurrencyType> GetById(long id);
}
