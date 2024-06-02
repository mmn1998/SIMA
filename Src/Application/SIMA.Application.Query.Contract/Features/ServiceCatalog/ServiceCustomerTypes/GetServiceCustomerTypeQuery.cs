using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;

public class GetServiceCustomerTypeQuery : IQuery<Result<GetServiceCustomerTypeQueryResult>>
{
    public long Id { get; set; }
}