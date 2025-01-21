using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;

public class GetBusinessCriticalityQuery : IQuery<Result<GetBusinessCriticalityQueryResult>>
{
    public long Id { get; set; }
}