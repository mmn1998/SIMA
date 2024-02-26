using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetAllStaffQuery : IQuery<Result<List<GetStaffQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
