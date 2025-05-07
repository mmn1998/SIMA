using SIMA.Application.Query.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ResponsibilityWageTypes;

namespace SIMA.Application.Query.Features.TrustyDrafts.ResponsibilityWageTypes;

public class ResponsibilityWageTypeQueryHandler : IQueryHandler<GetResponsibilityWageTypeQuery, Result<GetResponsibilityWageTypeQueryResult>>,
    IQueryHandler<GetAllResponsibilityWageTypesQuery, Result<IEnumerable<GetResponsibilityWageTypeQueryResult>>>
{
    private readonly IResponsibilityWageTypeQueryRepository _repository;

    public ResponsibilityWageTypeQueryHandler(IResponsibilityWageTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetResponsibilityWageTypeQueryResult>> Handle(GetResponsibilityWageTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetResponsibilityWageTypeQueryResult>>> Handle(GetAllResponsibilityWageTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}