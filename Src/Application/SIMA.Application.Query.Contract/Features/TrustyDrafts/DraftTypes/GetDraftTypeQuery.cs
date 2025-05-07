using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftTypes;

public class GetDraftTypeQuery : IQuery<Result<GetDraftTypeQueryResult>>
{
    public long Id { get; set; }
}