using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Assets;

public interface IAssetQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetAssetQueryResult>>> GetAll(GetAllAssetsQuery request);
    Task<Result<GetAssetQueryInfoResult>> GetByCode(GetAssetByCodeQuery request);
    Task<GetAssetQueryInfoResult> GetById(long id);
}