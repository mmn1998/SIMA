using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Contracts;

public interface IAccountTypeRepository : IRepository<AccountType>
{
    Task<AccountType> GetById(AccountTypeId id);
    Task<AccountType> GetByCode(string code);
}