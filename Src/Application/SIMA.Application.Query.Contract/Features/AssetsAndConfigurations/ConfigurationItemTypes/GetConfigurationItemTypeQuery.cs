using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemTypes;

public class GetConfigurationItemTypeQuery : IQuery<Result<GetConfigurationItemTypeQueryResult>>
{
    public long Id { get; set; }
}