using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetTypes;

public interface IAssetTypeQueryRepository : IQueryRepository
{
    Task<GetAssetTypeQueryResult> GetById(GetAssetTypeQuery request);
    Task<Result<IEnumerable<GetAssetTypeQueryResult>>> GetAll(GetAllAssetTypesQuery request);
}