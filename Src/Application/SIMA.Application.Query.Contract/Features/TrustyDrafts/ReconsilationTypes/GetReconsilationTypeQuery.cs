using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReconsilationTypes;

public class GetReconsilationTypeQuery : IQuery<Result<GetReconsilationTypeQueryResult>>
{
    public long Id { get; set; }
}