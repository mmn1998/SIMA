using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;

public interface ILoanTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, LoanTypeId? id = null);
}