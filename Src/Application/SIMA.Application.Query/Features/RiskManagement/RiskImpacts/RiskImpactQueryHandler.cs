using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskImpacts;

namespace SIMA.Application.Query.Features.RiskManagement.RiskImpacts
{
    public class RiskImpactQueryHandler :
    IQueryHandler<GetAllRiskImpactsQuery, Result<IEnumerable<GetRiskImpactsQueryResult>>>,
    IQueryHandler<GetRiskImpactQuery, Result<GetRiskImpactsQueryResult>>
    {
        private readonly IRiskImpactQueryRepository _repository;

        public RiskImpactQueryHandler(IRiskImpactQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetRiskImpactsQueryResult>>> Handle(GetAllRiskImpactsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetRiskImpactsQueryResult>> Handle(GetRiskImpactQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
