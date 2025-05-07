using SIMA.Application.Query.Contract.Features.BranchManagement.AccountTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.AccountTypes;

public interface IAccountTypeQueryRepository : IQueryRepository
{
    Task<GetAccountTypeQueryResult> GetById(GetAccountTypeQuery request);
    Task<Result<IEnumerable<GetAccountTypeQueryResult>>> GetAll(GetAllAccountTypesQuery request);
}