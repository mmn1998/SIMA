using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTechnicalStatuses;

public class GetAssetTechnicalStatusQuery : IQuery<Result<GetAssetTechnicalStatusQueryResult>>
{
    public long Id { get; set; }
}