using SIMA.Application.Query.Contract.Features.BCP.Consequences;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.Consequences;

namespace SIMA.Application.Query.Features.BCP.Consequences;

public class ConsequenceQueryHandler : IQueryHandler<GetConsequenceQuery, Result<GetConsequenceQueryResult>>,
    IQueryHandler<GetAllConsequencesQuery, Result<IEnumerable<GetConsequenceQueryResult>>>
{
    private readonly IConsequenceQueryRepository _repository;

    public ConsequenceQueryHandler(IConsequenceQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConsequenceQueryResult>> Handle(GetConsequenceQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConsequenceQueryResult>>> Handle(GetAllConsequencesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}