using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetAllStaffQuery : BaseRequest, IQuery<Result<IEnumerable<GetStaffQueryResult>>>
{
}
