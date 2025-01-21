using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageDeductionMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.WageDeductionMethods;

public interface IWageDeductionMethodQueryRepository : IQueryRepository
{
    Task<GetWageDeductionMethodQueryResult> GetById(GetWageDeductionMethodQuery request);
    Task<Result<IEnumerable<GetWageDeductionMethodQueryResult>>> GetAll(GetAllWageDeductionMethodsQuery request);
}