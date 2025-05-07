using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.AddressTypes;

public class GetAddressTypeQuery : IQuery<Result<GetAddressTypeQueryResult>>
{
    public long Id { get; set; }
}
