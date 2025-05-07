using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields
{
    public class GetConfigurationItemCustomFieldQuery : IQuery<Result<GetConfigurationItemCustomFieldQueryResult>>
    {
        public long Id { get; set; }
    }
}
