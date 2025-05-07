using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Contracts;

public interface IAccountTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AccountTypeId? id = null);
}