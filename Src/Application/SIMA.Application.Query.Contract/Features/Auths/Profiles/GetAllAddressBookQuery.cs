using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetAllAddressBookQuery :BaseRequest, IQuery<Result<IEnumerable<GetAddressBookQueryResult>>>
{
    public long Id { get; set; }
}
