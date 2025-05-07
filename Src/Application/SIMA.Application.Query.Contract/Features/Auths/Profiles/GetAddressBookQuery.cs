using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetAddressBookQuery : IQuery<Result<GetAddressBookQueryResult>>
{
    public long Id { get; set; }
}
