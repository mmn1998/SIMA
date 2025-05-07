using SIMA.Application.Query.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.AgentBankWageShareStatuses;

namespace SIMA.Application.Query.Features.TrustyDrafts.AgentBankWageShareStatuses;

public class AgentBankWageShareStatusQueryHandler : IQueryHandler<GetAgentBankWageShareStatusQuery, Result<GetAgentBankWageShareStatusQueryResult>>,
IQueryHandler<GetAllAgentBankWageShareStatusesQuery, Result<IEnumerable<GetAgentBankWageShareStatusQueryResult>>>
{
    private readonly IAgentBankWageShareStatusQueryRepository _repository;

    public AgentBankWageShareStatusQueryHandler(IAgentBankWageShareStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAgentBankWageShareStatusQueryResult>> Handle(GetAgentBankWageShareStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAgentBankWageShareStatusQueryResult>>> Handle(GetAllAgentBankWageShareStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
