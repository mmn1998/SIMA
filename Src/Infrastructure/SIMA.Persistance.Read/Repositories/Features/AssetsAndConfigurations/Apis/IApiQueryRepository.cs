using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Apis;

public interface IApiQueryRepository : IQueryRepository
{
    Task<GetApiQueryResult> GetById(GetApiQuery request);
    Task<Result<IEnumerable<GetApiQueryResult>>> GetAll(GetAllApisQuery request);
}