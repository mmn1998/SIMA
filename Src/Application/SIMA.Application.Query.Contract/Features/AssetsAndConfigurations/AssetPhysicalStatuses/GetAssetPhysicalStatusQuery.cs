using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetPhysicalStatuses;

public class GetAssetPhysicalStatusQuery : IQuery<Result<GetAssetPhysicalStatusQueryResult>>
{
    public long Id { get; set; }
}