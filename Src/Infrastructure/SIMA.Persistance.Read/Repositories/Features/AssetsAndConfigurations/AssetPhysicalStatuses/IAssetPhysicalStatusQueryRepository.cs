using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetPhysicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetPhysicalStatuses;

public interface IAssetPhysicalStatusQueryRepository : IQueryRepository
{
    Task<GetAssetPhysicalStatusQueryResult> GetById(GetAssetPhysicalStatusQuery request);
    Task<Result<IEnumerable<GetAssetPhysicalStatusQueryResult>>> GetAll(GetAllAssetPhysicalStatusesQuery request);
}