using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;

public class GetAllServiceCustomerTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetServiceCustomerTypeQueryResult>>>
{
}