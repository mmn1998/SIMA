using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCategories;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceCategories;

public class ServiceCategoryQueryHandler : IQueryHandler<GetServiceCategoryQuery, Result<GetServiceCategoryQueryResult>>,
    IQueryHandler<GetAllServiceCategoriesQuery, Result<IEnumerable<GetServiceCategoryQueryResult>>>,
    IQueryHandler<GetAllServiceCategoriesExceptThisIdQuery, Result<IEnumerable<GetServiceCategoryQueryResult>>>
{
    private readonly IServiceCategoryQueryRepository _repository;

    public ServiceCategoryQueryHandler(IServiceCategoryQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetServiceCategoryQueryResult>> Handle(GetServiceCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetServiceCategoryQueryResult>>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
    /// <summary>
    /// this is for this reason : in edit of service category form, same id should not be in the drop down of this form
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<IEnumerable<GetServiceCategoryQueryResult>>> Handle(GetAllServiceCategoriesExceptThisIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllExceptThisIdForParents(request.Id);
        return Result.Ok(result);
    }
}