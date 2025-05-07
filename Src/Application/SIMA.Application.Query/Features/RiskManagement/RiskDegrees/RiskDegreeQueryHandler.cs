using SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskDegrees;

namespace SIMA.Application.Query.Features.RiskManagement.RiskDegrees
{
    public class RiskDegreeQueryHandler :
    IQueryHandler<GetAllRiskDegreesQuery, Result<IEnumerable<GetRiskDegreesQueryResult>>>,
    IQueryHandler<GetRiskDegreeQuery, Result<GetRiskDegreesQueryResult>>
    {
        private readonly IRiskDegreeQueryRepository _repository;

        public RiskDegreeQueryHandler(IRiskDegreeQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetRiskDegreesQueryResult>>> Handle(GetAllRiskDegreesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetRiskDegreesQueryResult>> Handle(GetRiskDegreeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
