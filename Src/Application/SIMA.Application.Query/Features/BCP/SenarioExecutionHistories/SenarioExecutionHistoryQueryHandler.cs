using SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.SenarioExecutionHistories;

namespace SIMA.Application.Query.Features.BCP.SenarioExecutionHistories
{
    public class SenarioExecutionHistoryQueryHandler : IQueryHandler<GetSenarioExecutionHistoryQuery, Result<GetSenarioExecutionHistoryQueryResult>>,
    IQueryHandler<GetAllSenarioExecutionHistoriesQuery, Result<IEnumerable<GetSenarioExecutionHistoryQueryResult>>>
    {
        private readonly ISenarioExecutionHistoryQueryRepository _repository;

        public SenarioExecutionHistoryQueryHandler(ISenarioExecutionHistoryQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetSenarioExecutionHistoryQueryResult>> Handle(GetSenarioExecutionHistoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetSenarioExecutionHistoryQueryResult>>> Handle(GetAllSenarioExecutionHistoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
