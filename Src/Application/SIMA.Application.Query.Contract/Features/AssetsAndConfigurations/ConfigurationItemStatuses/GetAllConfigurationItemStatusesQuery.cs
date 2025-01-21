using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemStatuses;

public class GetAllConfigurationItemStatusesQuery : BaseRequest, IQuery<Result<IEnumerable<GetConfigurationItemStatusQueryResult>>>
{
}