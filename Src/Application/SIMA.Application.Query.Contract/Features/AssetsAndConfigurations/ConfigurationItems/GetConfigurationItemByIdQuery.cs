using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;

public class GetConfigurationItemByIdQuery : IQuery<Result<GetConfigurationItemQueryInfoResult>>
{
    public long Id { get; set; }
}
