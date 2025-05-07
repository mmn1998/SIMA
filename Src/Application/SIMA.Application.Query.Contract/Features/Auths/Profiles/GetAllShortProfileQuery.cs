using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetAllShortProfileQuery : IQuery<Result<List<GetShortProfileQueryResult>>>
{
    public long Id { get; set; }
}
