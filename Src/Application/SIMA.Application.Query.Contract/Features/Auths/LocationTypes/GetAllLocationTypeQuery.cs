using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.LocationTypes;

public class GetAllLocationTypeQuery : IQuery<Result<List<GetLocationTypeQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
