using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.AddressTypes;

public class GetAllAddressTypesQuery : IQuery<Result<List<GetAddressTypeQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
