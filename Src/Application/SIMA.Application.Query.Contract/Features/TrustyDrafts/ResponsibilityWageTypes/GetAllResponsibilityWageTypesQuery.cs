using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;

public class GetAllResponsibilityWageTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetResponsibilityWageTypeQueryResult>>>
{
}