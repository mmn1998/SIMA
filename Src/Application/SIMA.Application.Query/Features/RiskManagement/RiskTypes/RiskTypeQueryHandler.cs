using SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskTypes;

namespace SIMA.Application.Query.Features.RiskManagement.RiskTypes
{
    public class RiskTypeQueryHandler :
    IQueryHandler<GetAllRiskTypesQuery, Result<IEnumerable<GetRiskTypesQueryResult>>>,
    IQueryHandler<GetRiskTypeQuery, Result<GetRiskTypesQueryResult>>
    {
        private readonly IRiskTypeQueryRepository _repository;

        public RiskTypeQueryHandler(IRiskTypeQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetRiskTypesQueryResult>>> Handle(GetAllRiskTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetRiskTypesQueryResult>> Handle(GetRiskTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
