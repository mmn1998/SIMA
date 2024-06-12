using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevelMeasures;

namespace SIMA.Application.Query.Features.RiskManagement.RiskLevelMeasures
{
    public class RiskLevelMeasureQueryHandler :
    IQueryHandler<GetAllRiskLevelMeasuresQuery, Result<IEnumerable<GetAllRiskLevelMeasuresQueryResult>>>,
    IQueryHandler<GetRiskLevelMeasureQuery, Result<GetRiskLevelMeasuresQueryResult>>
    {
        private readonly IRiskLevelMeasureQueryRepository _repository;

        public RiskLevelMeasureQueryHandler(IRiskLevelMeasureQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetAllRiskLevelMeasuresQueryResult>>> Handle(GetAllRiskLevelMeasuresQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetRiskLevelMeasuresQueryResult>> Handle(GetRiskLevelMeasureQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
