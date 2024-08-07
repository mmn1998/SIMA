using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCategories;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceCategories;

public class ServiceCategoryQueryHandler : IQueryHandler<GetServiceCategoryQuery, Result<GetServiceCategoryQueryResult>>,
    IQueryHandler<GetAllServiceCategoriesQuery, Result<IEnumerable<GetServiceCategoryQueryResult>>>,
    IQueryHandler<GetAllServiceCategoriesByServiceTypeIdQuery, Result<IEnumerable<GetServiceCategoryQueryResult>>>
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

    public async Task<Result<IEnumerable<GetServiceCategoryQueryResult>>> Handle(GetAllServiceCategoriesByServiceTypeIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByServiceTypeId(request.ServiceTypeId);
    }
}