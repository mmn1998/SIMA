using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.RequestValors;

public class GetRequestValorQuery : IQuery<Result<GetRequestValorQueryResult>>
{
    public long Id { get; set; }
}