using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;

public class GetAllDraftForPayment : BaseRequest, IQuery<Result<IEnumerable<GetAllDraftForPaymentResult>>>
{

}