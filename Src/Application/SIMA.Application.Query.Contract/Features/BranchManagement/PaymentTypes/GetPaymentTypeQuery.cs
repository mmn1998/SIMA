using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;

public class GetPaymentTypeQuery : IQuery<Result<GetPaymentTypeQueryResult>>
{
    public long Id { get; set; }
}
