using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields
{
    public class GetAssetCustomFieldQuery : IQuery<Result<GetAssetCustomFieldQueryResult>>
    {
        public long Id { get; set; }
    }
}
