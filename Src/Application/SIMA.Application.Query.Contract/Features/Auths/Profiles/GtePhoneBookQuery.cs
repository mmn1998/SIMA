using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GtePhoneBookQuery : IQuery<Result<GetPhoneBookQueryResult>>
{
    public long Id { get; set; }
}
