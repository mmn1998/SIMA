using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields
{
    public class GetAllAssetCustomFieldsQuery : BaseRequest, IQuery<Result<IEnumerable<GetAssetCustomFieldQueryResult>>>
    {
    }
}
