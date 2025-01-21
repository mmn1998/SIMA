using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;

public class GetTrustyDraftQuery : IQuery<Result<GetTrustyDraftQueryResult>>
{
    public long Id { get; set; }
}