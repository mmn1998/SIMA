using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.FinancialActionTypes
{
    public interface IFinancialActionTypeQueryRepository : IQueryRepository
    {
        Task<GetFinancialActionTypeQueryResult> GetById(long id);
        Task<Result<IEnumerable<GetFinancialActionTypeQueryResult>>> GetAll(GetAllFinancialActionTypesQuery request);
    }
}
