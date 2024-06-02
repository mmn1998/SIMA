using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceUserTypes;

public class GetServiceUserTypeQuery : IQuery<Result<GetServiceUserTypeQueryResult>>
{
    public long Id { get; set; }
}