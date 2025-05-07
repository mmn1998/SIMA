using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.WageRates;

namespace SIMA.Application.Query.Features.TrustyDrafts.WageRates;

public class WageRateQueryHandler : IQueryHandler<GetWageRateQuery, Result<GetWageRateQueryResult>>, 
    IQueryHandler<GetWageCalculatorQuery, Result<GetWageCalculatorQueryResult>>,
    IQueryHandler<GetAllWageRatesQuery, Result<IEnumerable<GetWageRateQueryResult>>>,
    IQueryHandler<GetAllWageRatesByCurrencyTypeIdQuery, Result<IEnumerable<GetWageRateQueryResult>>>
{
    private readonly IWageRateQueryRepository _repository;

    public WageRateQueryHandler(IWageRateQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetWageRateQueryResult>> Handle(GetWageRateQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetWageRateQueryResult>>> Handle(GetAllWageRatesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetWageCalculatorQueryResult>> Handle(GetWageCalculatorQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.CalculateWage(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetWageRateQueryResult>>> Handle(GetAllWageRatesByCurrencyTypeIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllByCurrencyTypeId(request.CurrencyTypeId);
        return Result.Ok(result);
    }
}