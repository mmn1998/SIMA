using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields
{
    public class GetAllConfigurationItemCustomFieldsQuery : BaseRequest, IQuery<Result<IEnumerable<GetConfigurationItemCustomFieldQueryResult>>>
    {
    }
}
