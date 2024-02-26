using SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;

namespace SIMA.Application.Query.Features.IssueManagement.IssueWeightCategories;

public class IssueWeightCategoryQueryHandler : 
    IQueryHandler<GetAllIssueWeightCategoriesQuery, Result<List<GetIssueWeightCategoryQueryResult>>>,
    IQueryHandler<GetIssueWeightCategoryByWeightQuery, Result<string>>,
    IQueryHandler<GetIssueWeightCategoryQuery, Result<GetIssueWeightCategoryQueryResult>>
{
    private readonly IIssueWeightCategoryQueryRepository _repository;

    public IssueWeightCategoryQueryHandler(IIssueWeightCategoryQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetIssueWeightCategoryQueryResult>>> Handle(GetAllIssueWeightCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetIssueWeightCategoryQueryResult>> Handle(GetIssueWeightCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<string>> Handle(GetIssueWeightCategoryByWeightQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByWeight(request.Weight);
        return Result.Ok(result);
    }
}
