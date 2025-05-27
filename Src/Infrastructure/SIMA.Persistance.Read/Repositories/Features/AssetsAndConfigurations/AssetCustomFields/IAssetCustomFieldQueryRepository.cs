using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetCustomFields
{
    public interface IAssetCustomFieldQueryRepository : IQueryRepository
    {
        Task<GetAssetCustomFieldQueryResult> GetById(GetAssetCustomFieldQuery request);
        Task<Result<IEnumerable<GetAssetCustomFieldQueryResult>>> GetAll(GetAllAssetCustomFieldsQuery request);
    }
}
