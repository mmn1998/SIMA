using SIMA.Application.Query.Contract.Features.BCP.ConsequenceValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceValues;

namespace SIMA.Application.Query.Features.BCP.ConsequenceValues;

public class ConsequenceValueQueryHandler : IQueryHandler<GetConsequenceValueQuery, Result<GetConsequenceValueQueryResult>>,
    IQueryHandler<GetAllConsequenceValuesQuery, Result<IEnumerable<GetConsequenceValueQueryResult>>>
{
    private readonly IConsequenceValueQueryRepository _repository;

    public ConsequenceValueQueryHandler(IConsequenceValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConsequenceValueQueryResult>> Handle(GetConsequenceValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConsequenceValueQueryResult>>> Handle(GetAllConsequenceValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}