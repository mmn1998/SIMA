using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Positions
{
    public class GetPositionByDepartemantQuery : IQuery<Result<IEnumerable<GetPositionQueryResult>>>
    {
        public long DepartmentId { get; set; }
    }
}
