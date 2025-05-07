using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;

public class GetChannelQuery : IQuery<Result<GetChannelQueryResult>>
{
    public long Id { get; set; }
}