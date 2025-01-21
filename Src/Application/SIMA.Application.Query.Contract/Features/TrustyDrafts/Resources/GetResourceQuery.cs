using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.Resources;

public class GetResourceQuery : IQuery<Result<GetResourceQueryResult>>
{
    public long Id { get; set; }
}