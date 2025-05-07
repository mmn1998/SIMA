using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.CancellationResaons;

public class GetCancellationResaonQuery : IQuery<Result<GetCancellationResaonQueryResult>>
{
    public long Id { get; set; }
}