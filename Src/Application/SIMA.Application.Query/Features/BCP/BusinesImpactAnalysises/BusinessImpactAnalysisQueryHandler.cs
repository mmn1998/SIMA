using SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.BusinesImpactAnalysises;

namespace SIMA.Application.Query.Features.BCP.BusinesImpactAnalysises;

public class BusinessImpactAnalysisQueryHandler : IQueryHandler<GetBusinessImpactAnalysisQuery, Result<GetBusinessImpactAnalysisQueryResult>>,
    IQueryHandler<GetAllBusinessImpactAnalysisesQuery, Result<IEnumerable<GetAllBusinessImpactAnalysisesQueryResult>>>
{
    private readonly IBusinessImpactAnalysisQueryRepository _repository;

    public BusinessImpactAnalysisQueryHandler(IBusinessImpactAnalysisQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBusinessImpactAnalysisQueryResult>> Handle(GetBusinessImpactAnalysisQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllBusinessImpactAnalysisesQueryResult>>> Handle(GetAllBusinessImpactAnalysisesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}