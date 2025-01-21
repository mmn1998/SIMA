using SIMA.Application.Query.Contract.Features.BranchManagement.LoanTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.LoanTypes;

public interface ILoanTypeQueryRepository : IQueryRepository
{
    Task<GetLoanTypeQueryResult> GetById(GetLoanTypeQuery request);
    Task<Result<IEnumerable<GetLoanTypeQueryResult>>> GetAll(GetAllLoanTypesQuery request);
}