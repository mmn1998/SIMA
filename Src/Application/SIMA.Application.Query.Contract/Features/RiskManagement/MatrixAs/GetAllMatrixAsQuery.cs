using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;

public class GetAllMatrixAsQuery : BaseRequest, IQuery<Result<IEnumerable<GetMatrixAQueryResult>>>
{
}