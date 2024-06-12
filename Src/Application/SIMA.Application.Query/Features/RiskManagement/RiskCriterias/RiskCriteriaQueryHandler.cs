using SIMA.Application.Query.Contract.Features.RiskManagement.RiskCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskCriterias;

namespace SIMA.Application.Query.Features.RiskManagement.RiskCriterias
{
    public class RiskCriteriaQueryHandler :
    IQueryHandler<GetAllRiskCriteriasQuery, Result<IEnumerable<GetAllRiskCriteriasQueryResult>>>,
    IQueryHandler<GetRiskCriteriaQuery, Result<GetRiskCriteriaQueryResult>>
    {
        private readonly IRiskCriteriaQueryRepository _repository;

        public RiskCriteriaQueryHandler(IRiskCriteriaQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetAllRiskCriteriasQueryResult>>> Handle(GetAllRiskCriteriasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetRiskCriteriaQueryResult>> Handle(GetRiskCriteriaQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
