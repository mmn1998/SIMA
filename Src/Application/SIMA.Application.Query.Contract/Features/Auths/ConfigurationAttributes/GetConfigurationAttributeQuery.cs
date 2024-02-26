using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;

public class GetConfigurationAttributeQuery : IQuery<Result<GetConfigurationAttributeQueryResult>>
{
    public long Id { get; set; }
}
