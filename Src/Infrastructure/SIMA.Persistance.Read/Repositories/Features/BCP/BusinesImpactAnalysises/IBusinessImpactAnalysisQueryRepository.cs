using SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BusinesImpactAnalysises;

public interface IBusinessImpactAnalysisQueryRepository : IQueryRepository
{
    Task<GetBusinessImpactAnalysisQueryResult> GetById(GetBusinessImpactAnalysisQuery request);
    Task<Result<IEnumerable<GetAllBusinessImpactAnalysisesQueryResult>>> GetAll(GetAllBusinessImpactAnalysisesQuery request);
}