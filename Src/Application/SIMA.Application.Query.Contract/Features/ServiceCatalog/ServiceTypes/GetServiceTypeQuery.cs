using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;

public class GetServiceTypeQuery : IQuery<Result<GetServiceTypesQueryResult>>
{
    public long Id { get; set; }
}
