using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;

public class GetChannelByCodeQuery : IQuery<Result<GetChannelQueryResult>>
{
    public string Code { get; set; }
}