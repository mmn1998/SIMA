using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Domains;

public class GetDomainQuery : IQuery<Result<GetDomainQueryResult>>
{
    public long Id { get; set; }
}
