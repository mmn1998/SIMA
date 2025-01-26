using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.ConsequenceCategories;

namespace SIMA.Application.Query.Features.RiskManagement.ConsequenceCategories;

public class ConsequenceCategoryQueryHandler : IQueryHandler<GetConsequenceCategoryQuery, Result<GetConsequenceCategoryQueryResult>>,
    IQueryHandler<GetAllConsequenceCategoriesQuery, Result<IEnumerable<GetConsequenceCategoryQueryResult>>>
{
    private readonly IConsequenceCategoryQueryRepository _repository;

    public ConsequenceCategoryQueryHandler(IConsequenceCategoryQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConsequenceCategoryQueryResult>> Handle(GetConsequenceCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConsequenceCategoryQueryResult>>> Handle(GetAllConsequenceCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}