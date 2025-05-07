using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;

public interface ICurrencyTypeReadRepository : IQueryRepository
{
    Task<GetCurrencyTypeQueryResult> GetById(long id);
    Task<Result<IEnumerable<GetCurrencyTypeQueryResult>>> GetAll(GetAllCurrencyTypesQuery request);
}
