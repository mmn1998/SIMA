using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Categories;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.Categories;

public class CategoryQueryHandler: IQueryHandler<GetCategoryQuery, Result<GetCategoryQueryResult>>,
    IQueryHandler<GetAllCategoryQuery, Result<IEnumerable<GetCategoryQueryResult>>>
{
    private readonly ICategoryQueryRepository _repository;

    public CategoryQueryHandler(ICategoryQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetCategoryQueryResult>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCategoryQueryResult>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}