using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemTypes;

public class GetAllConfigurationItemTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetConfigurationItemTypeQueryResult>>>
{
}