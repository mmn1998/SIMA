using SIMA.Application.Query.Contract.Features.RiskManagement.CobitCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.CobitCategories;

namespace SIMA.Application.Query.Features.RiskManagement.CobitCategories;

public class CobitCategoryQueryHandler : IQueryHandler<GetCobitCategoryQuery, Result<GetCobitCategoryQueryResult>>,
    IQueryHandler<GetAllCobitCategoriesQuery, Result<IEnumerable<GetCobitCategoryQueryResult>>>
{
    private readonly ICobitCategoryQueryRepository _repository;

    public CobitCategoryQueryHandler(ICobitCategoryQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCobitCategoryQueryResult>> Handle(GetCobitCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCobitCategoryQueryResult>>> Handle(GetAllCobitCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}