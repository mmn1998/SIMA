using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.WageRates;

public interface IWageRateQueryRepository : IQueryRepository
{
    Task<GetWageRateQueryResult> GetById(GetWageRateQuery request);
    Task<GetWageCalculatorQueryResult> CalculateWage(GetWageCalculatorQuery request);
    Task<Result<IEnumerable<GetWageRateQueryResult>>> GetAll(GetAllWageRatesQuery request);
    Task<IEnumerable<GetWageRateQueryResult>> GetAllByCurrencyTypeId(long currencyTypeId);
}