using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems
{
    public class GetAllDataBaseConfigurationItemQuery : BaseRequest, IQuery<Result<IEnumerable<GetConfigurationItemQueryResult>>>
    {
    }
}
