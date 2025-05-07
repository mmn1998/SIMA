using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.CurrencyOprationTypes
{
    public interface ICurrencyOprationTypeQueryRepository : IQueryRepository
    {
        Task<GetCurrencyOprationTypeQueryResult> GetById(long id);
        Task<Result<IEnumerable<GetCurrencyOprationTypeQueryResult>>> GetAll(GetAllCurrencyOprationTypesQuery request);
    }
}
