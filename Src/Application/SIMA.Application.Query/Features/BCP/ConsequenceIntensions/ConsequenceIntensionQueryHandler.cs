using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceIntensions;

namespace SIMA.Application.Query.Features.BCP.ConsequenceIntensions;

public class ConsequenceIntensionQueryHandler : IQueryHandler<GetConsequenceIntensionQuery, Result<GetConsequenceIntensionQueryResult>>,
    IQueryHandler<GetAllConsequenceIntensionsQuery, Result<IEnumerable<GetConsequenceIntensionQueryResult>>>
{
    private readonly IConsequenceIntensionQueryRepository _repository;

    public ConsequenceIntensionQueryHandler(IConsequenceIntensionQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConsequenceIntensionQueryResult>> Handle(GetConsequenceIntensionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConsequenceIntensionQueryResult>>> Handle(GetAllConsequenceIntensionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}