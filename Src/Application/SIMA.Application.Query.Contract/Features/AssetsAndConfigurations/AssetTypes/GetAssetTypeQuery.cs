using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;

public class GetAssetTypeQuery : IQuery<Result<GetAssetTypeQueryResult>>
{
    public long Id { get; set; }
}