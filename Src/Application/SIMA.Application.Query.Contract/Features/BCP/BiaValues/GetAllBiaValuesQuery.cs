using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BiaValues;

public class GetAllBiaValuesQuery : BaseRequest, IQuery<Result<IEnumerable<GetBiaValueQueryResult>>>
{
}