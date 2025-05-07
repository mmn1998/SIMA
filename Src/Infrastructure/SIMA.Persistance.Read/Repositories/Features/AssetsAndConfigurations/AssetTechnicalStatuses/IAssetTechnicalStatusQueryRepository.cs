using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTechnicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetTechnicalStatuses;

public interface IAssetTechnicalStatusQueryRepository : IQueryRepository
{
    Task<GetAssetTechnicalStatusQueryResult> GetById(GetAssetTechnicalStatusQuery request);
    Task<Result<IEnumerable<GetAssetTechnicalStatusQueryResult>>> GetAll(GetAllAssetTechnicalStatusesQuery request);
}