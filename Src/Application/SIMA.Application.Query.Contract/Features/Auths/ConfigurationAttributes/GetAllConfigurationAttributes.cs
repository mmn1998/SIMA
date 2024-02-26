using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;

public class GetAllConfigurationAttributes : IQuery<Result<List<GetConfigurationAttributeQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
