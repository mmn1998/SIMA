using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.PaymentTypes;

public class DeletePaymentTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
