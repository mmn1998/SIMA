using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageDeductionMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.WageDeductionMethods;

namespace SIMA.Application.Query.Features.TrustyDrafts.WageDeductionMethods;

public class WageDeductionMethodQueryHandler : IQueryHandler<GetWageDeductionMethodQuery, Result<GetWageDeductionMethodQueryResult>>,
    IQueryHandler<GetAllWageDeductionMethodsQuery, Result<IEnumerable<GetWageDeductionMethodQueryResult>>>
{
    private readonly IWageDeductionMethodQueryRepository _repository;

    public WageDeductionMethodQueryHandler(IWageDeductionMethodQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetWageDeductionMethodQueryResult>> Handle(GetWageDeductionMethodQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetWageDeductionMethodQueryResult>>> Handle(GetAllWageDeductionMethodsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}