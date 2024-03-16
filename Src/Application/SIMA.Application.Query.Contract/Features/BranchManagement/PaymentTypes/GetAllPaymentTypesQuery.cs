using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;

public class GetAllPaymentTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetPaymentTypeQueryResult>>>
{
}
