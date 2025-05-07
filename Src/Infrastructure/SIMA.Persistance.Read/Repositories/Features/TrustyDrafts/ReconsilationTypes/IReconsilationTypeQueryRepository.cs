using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReconsilationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ReconsilationTypes;

public interface IReconsilationTypeQueryRepository : IQueryRepository
{
    Task<GetReconsilationTypeQueryResult> GetById(GetReconsilationTypeQuery request);
    Task<Result<IEnumerable<GetReconsilationTypeQueryResult>>> GetAll(GetAllReconsilationTypesQuery request);
}