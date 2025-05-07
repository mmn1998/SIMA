using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetAllPhoneBookQuery :BaseRequest, IQuery<Result<IEnumerable<GetPhoneBookQueryResult>>>
{
    public long Id { get; set; }
}