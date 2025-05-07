using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;
public class GetBusinessEntityQuery : IQuery<Result<GetBusinessEntityQueryResult>>
{
    public long Id { get; set; }
}