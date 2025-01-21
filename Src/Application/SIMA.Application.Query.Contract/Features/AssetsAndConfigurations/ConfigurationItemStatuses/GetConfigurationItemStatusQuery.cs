using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemStatuses;

public class GetConfigurationItemStatusQuery : IQuery<Result<GetConfigurationItemStatusQueryResult>>
{
    public long Id { get; set; }
}