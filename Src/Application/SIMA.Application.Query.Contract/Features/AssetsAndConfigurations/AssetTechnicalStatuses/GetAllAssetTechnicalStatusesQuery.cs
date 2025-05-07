using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTechnicalStatuses;

public class GetAllAssetTechnicalStatusesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAssetTechnicalStatusQueryResult>>>
{
}