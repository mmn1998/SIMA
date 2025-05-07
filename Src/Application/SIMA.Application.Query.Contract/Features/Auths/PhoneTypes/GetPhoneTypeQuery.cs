using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;

public class GetPhoneTypeQuery : IQuery<Result<GetPhoneTypeQueryResult>>
{
    public long Id { get; set; }
}
