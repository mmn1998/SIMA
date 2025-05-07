using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.EvaluationCriterias;

namespace SIMA.Application.Query.Features.RiskManagement.EvaluationCriterias;

public class EvaluationCriteriaQueryHandler : IQueryHandler<GetAllEvaluationCriteriasQuery, Result<IEnumerable<GetEvaluationCriteriaQueryResult>>>,
    IQueryHandler<GetEvaluationCriteriaQuery, Result<GetEvaluationCriteriaQueryResult>>

{
    private readonly IEvaluationCriteriaQueryRepository _repository;

    public EvaluationCriteriaQueryHandler(IEvaluationCriteriaQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetEvaluationCriteriaQueryResult>>> Handle(GetAllEvaluationCriteriasQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetEvaluationCriteriaQueryResult>> Handle(GetEvaluationCriteriaQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
