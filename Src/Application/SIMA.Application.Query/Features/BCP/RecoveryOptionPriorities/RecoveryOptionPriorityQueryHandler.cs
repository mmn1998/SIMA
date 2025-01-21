using SIMA.Application.Query.Contract.Features.BCP.RecoveryOptionPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.RecoveryOptionPriorities;

namespace SIMA.Application.Query.Features.BCP.RecoveryOptionPriorities;

public class RecoveryOptionPriorityQueryHandler : IQueryHandler<GetRecoveryOptionPriorityQuery, Result<GetRecoveryOptionPriorityQueryResult>>,
    IQueryHandler<GetAllRecoveryOptionPrioritiesQuery, Result<IEnumerable<GetRecoveryOptionPriorityQueryResult>>>
{
    private readonly IRecoveryOptionPriorityQueryRepository _repository;

    public RecoveryOptionPriorityQueryHandler(IRecoveryOptionPriorityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetRecoveryOptionPriorityQueryResult>> Handle(GetRecoveryOptionPriorityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetRecoveryOptionPriorityQueryResult>>> Handle(GetAllRecoveryOptionPrioritiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}