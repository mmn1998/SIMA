using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;

public interface ILoanTypeRepository : IRepository<LoanType>
{
    Task<LoanType> GetById(LoanTypeId id);

    Task<LoanType> GetByCode(string code);
}