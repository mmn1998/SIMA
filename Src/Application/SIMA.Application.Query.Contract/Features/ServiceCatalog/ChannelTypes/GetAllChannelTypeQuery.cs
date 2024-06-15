using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ChannelTypes;

public class GetAllChannelTypeQuery : BaseRequest, IQuery<Result<IEnumerable<GetChannelTypeQueryResult>>>
{
}