using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;

public interface ICurrencyTypeReadRepository : IQueryRepository
{
    Task<GetCurrencyTypeQueryResult> GetById(long id);
    Task<List<GetCurrencyTypeQueryResult>> GetAll(BaseRequest request);
}
