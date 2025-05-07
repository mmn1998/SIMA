using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftValorStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftValorStatuses;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftValorStatuses;

public class DraftValorStatusQueryHandler : IQueryHandler<GetDraftValorStatusQuery, Result<GetDraftValorStatusQueryResult>>,
    IQueryHandler<GetAllDraftValorStatusesQuery, Result<IEnumerable<GetDraftValorStatusQueryResult>>>
{
    private readonly IDraftValorStatusQueryRepository _repository;

    public DraftValorStatusQueryHandler(IDraftValorStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftValorStatusQueryResult>> Handle(GetDraftValorStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftValorStatusQueryResult>>> Handle(GetAllDraftValorStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}