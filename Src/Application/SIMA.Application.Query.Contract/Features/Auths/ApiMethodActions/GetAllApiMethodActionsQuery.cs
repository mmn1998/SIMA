using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System.Collections.Generic;

namespace SIMA.Application.Query.Contract.Features.Auths.ApiMethodActions;

public class GetAllApiMethodActionsQuery : BaseRequest, IQuery<Result<IEnumerable<GetApiMethodActionQueryResult>>>
{
}

