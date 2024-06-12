using SIMA.Application.Query.Contract.Features.RiskManagement.ImpactScales;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.ImpactScales;

namespace SIMA.Application.Query.Features.RiskManagement.ImpactScales
{
    public class ImpactScaleQueryHandler :
    IQueryHandler<GetAllImpactScalesQuery, Result<IEnumerable<GetImpactScalesQueryResult>>>,
    IQueryHandler<GetImpactScaleQuery, Result<GetImpactScalesQueryResult>>
    {
        private readonly IImpactScaleQueryRepository _repository;

        public ImpactScaleQueryHandler(IImpactScaleQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetImpactScalesQueryResult>>> Handle(GetAllImpactScalesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetImpactScalesQueryResult>> Handle(GetImpactScaleQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
