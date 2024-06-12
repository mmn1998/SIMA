using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ChannelTypes;

public class GetChannelTypeQuery : IQuery<Result<GetChannelTypeQueryResult>>
{
    public long Id { get; set; }
}