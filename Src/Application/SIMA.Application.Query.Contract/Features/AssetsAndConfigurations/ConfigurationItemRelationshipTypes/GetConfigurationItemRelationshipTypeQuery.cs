using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;

public class GetConfigurationItemRelationshipTypeQuery : IQuery<Result<GetConfigurationItemRelationshipTypeQueryResult>>
{
    public long Id { get; set; }
}