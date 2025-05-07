using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.Scenarios;

namespace SIMA.Application.Query.Features.BCP.Scenarios
{
    public class ScenarioQueryHandler : IQueryHandler<GetScenarioQuery, Result<GetScenarioQueryResult>>,
    IQueryHandler<GetAllScenariosQuery, Result<IEnumerable<GetScenarioQueryResult>>>
    {
        private readonly IScenarioQueryRepository _repository;

        public ScenarioQueryHandler(IScenarioQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetScenarioQueryResult>> Handle(GetScenarioQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetScenarioQueryResult>>> Handle(GetAllScenariosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
