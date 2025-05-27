using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceIntensionDescriptions;

namespace SIMA.Application.Query.Features.BCP.ConsequenceIntensionDescriptions;

public class ConsequenceIntensionDescriptionQueryHandler : IQueryHandler<GetConsequenceIntensionDescriptionQuery, Result<GetConsequenceIntensionDescriptionQueryResult>>,
    IQueryHandler<GetAllConsequenceIntensionDescriptionsQuery, Result<IEnumerable<GetConsequenceIntensionDescriptionQueryResult>>>
{
    private readonly IConsequenceIntensionDescriptionQueryRepository _repository;

    public ConsequenceIntensionDescriptionQueryHandler(IConsequenceIntensionDescriptionQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConsequenceIntensionDescriptionQueryResult>> Handle(GetConsequenceIntensionDescriptionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConsequenceIntensionDescriptionQueryResult>>> Handle(GetAllConsequenceIntensionDescriptionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}