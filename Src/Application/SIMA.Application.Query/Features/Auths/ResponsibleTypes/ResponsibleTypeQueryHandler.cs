using SIMA.Application.Query.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.ResponsibleTypes;

namespace SIMA.Application.Query.Features.Auths.ResponsibleTypes;

public class ResponsibleTypeQueryHandler : IQueryHandler<GetResponsibleTypeQuery, Result<GetResponsibleTypeQueryResult>>,
    IQueryHandler<GetAllResponsibleTypeQuery, Result<IEnumerable<GetResponsibleTypeQueryResult>>>
{
    private readonly IResponsibleTypeQueryRepository _repository;

    public ResponsibleTypeQueryHandler(IResponsibleTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetResponsibleTypeQueryResult>> Handle(GetResponsibleTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetResponsibleTypeQueryResult>>> Handle(GetAllResponsibleTypeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}