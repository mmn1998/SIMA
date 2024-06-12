using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevels;

namespace SIMA.Application.Query.Features.RiskManagement.RiskLevels
{
    public class RiskLevelQueryHandler :
    IQueryHandler<GetAllRiskLevelsQuery, Result<IEnumerable<GetRiskLevelsQueryResult>>>,
    IQueryHandler<GetRiskLevelQuery, Result<GetRiskLevelsQueryResult>>
    {
        private readonly IRiskLevelQueryRepository _repository;

        public RiskLevelQueryHandler(IRiskLevelQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetRiskLevelsQueryResult>>> Handle(GetAllRiskLevelsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetRiskLevelsQueryResult>> Handle(GetRiskLevelQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
