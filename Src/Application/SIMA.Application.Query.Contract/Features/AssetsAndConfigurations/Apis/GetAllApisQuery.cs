using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;

public class GetAllApisQuery : BaseRequest, IQuery<Result<IEnumerable<GetApiQueryResult>>>
{
}