using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftStatuses;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftStatuses;

public class DraftStatusQueryHandler : IQueryHandler<GetDraftStatusQuery, Result<GetDraftStatusQueryResult>>,
    IQueryHandler<GetAllDraftStatusesQuery, Result<IEnumerable<GetDraftStatusQueryResult>>>
{
    private readonly IDraftStatusQueryRepository _repository;

    public DraftStatusQueryHandler(IDraftStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftStatusQueryResult>> Handle(GetDraftStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftStatusQueryResult>>> Handle(GetAllDraftStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}