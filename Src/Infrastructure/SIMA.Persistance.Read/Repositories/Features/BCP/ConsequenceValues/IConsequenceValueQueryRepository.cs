using SIMA.Application.Query.Contract.Features.BCP.ConsequenceValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceValues;

public interface IConsequenceValueQueryRepository : IQueryRepository
{
    Task<GetConsequenceValueQueryResult> GetById(GetConsequenceValueQuery request);
    Task<Result<IEnumerable<GetConsequenceValueQueryResult>>> GetAll(GetAllConsequenceValuesQuery request);
}