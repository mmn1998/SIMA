using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.BranchManagement.PaymentTypes;

public class PaymentTypeQueryHandler : IQueryHandler<GetAllPaymentTypesQuery, Result<IEnumerable<GetPaymentTypeQueryResult>>>,
    IQueryHandler<GetPaymentTypeQuery, Result<GetPaymentTypeQueryResult>>
{
    private readonly IPaymentTypeReadRepository _repository;

    public PaymentTypeQueryHandler(IPaymentTypeReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetPaymentTypeQueryResult>>> Handle(GetAllPaymentTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetPaymentTypeQueryResult>> Handle(GetPaymentTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
