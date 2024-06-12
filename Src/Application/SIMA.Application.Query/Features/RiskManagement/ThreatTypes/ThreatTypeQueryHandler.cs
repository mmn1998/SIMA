using SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.ThreatTypes;

namespace SIMA.Application.Query.Features.RiskManagement.ThreatTypes
{
    public class ThreatTypeQueryHandler :
    IQueryHandler<GetAllThreatTypesQuery, Result<IEnumerable<GetThreatTypesQueryResult>>>,
    IQueryHandler<GetThreatTypeQuery, Result<GetThreatTypesQueryResult>>
    {
        private readonly IThreatTypeQueryRepository _repository;

        public ThreatTypeQueryHandler(IThreatTypeQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetThreatTypesQueryResult>>> Handle(GetAllThreatTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetThreatTypesQueryResult>> Handle(GetThreatTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
