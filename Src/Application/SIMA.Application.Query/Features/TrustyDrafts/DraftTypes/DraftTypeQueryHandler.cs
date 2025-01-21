using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftTypes;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftTypes;

public class DraftTypeQueryHandler : IQueryHandler<GetDraftTypeQuery, Result<GetDraftTypeQueryResult>>,
    IQueryHandler<GetAllDraftTypesQuery, Result<IEnumerable<GetDraftTypeQueryResult>>>
{
    private readonly IDraftTypeQueryRepository _repository;

    public DraftTypeQueryHandler(IDraftTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftTypeQueryResult>> Handle(GetDraftTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftTypeQueryResult>>> Handle(GetAllDraftTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}