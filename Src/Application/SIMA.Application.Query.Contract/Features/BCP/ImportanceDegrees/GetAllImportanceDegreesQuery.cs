using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ImportanceDegrees;

public class GetAllImportanceDegreesQuery : BaseRequest, IQuery<Result<IEnumerable<GetImportanceDegreeQueryResult>>>
{
}