using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;

public class GetResponsibilityWageTypeQuery : IQuery<Result<GetResponsibilityWageTypeQueryResult>>
{
    public long Id { get; set; }
}