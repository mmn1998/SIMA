using SIMA.Application.Query.Contract.Features.BCP.BiaValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BiaValues;

public interface IBiaValueQueryRepository : IQueryRepository
{
    Task<GetBiaValueQueryResult> GetById(GetBiaValueQuery request);
    Task<Result<IEnumerable<GetBiaValueQueryResult>>> GetAll(GetAllBiaValuesQuery request);
}