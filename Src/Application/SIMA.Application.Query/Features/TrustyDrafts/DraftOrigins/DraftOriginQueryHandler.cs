using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftOrigins;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftOrigins;

public class DraftOriginQueryHandler : IQueryHandler<GetDraftOriginQuery, Result<GetDraftOriginQueryResult>>,
    IQueryHandler<GetAllDraftOriginsQuery, Result<IEnumerable<GetDraftOriginQueryResult>>>
{
    private readonly IDraftOriginQueryRepository _repository;

    public DraftOriginQueryHandler(IDraftOriginQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftOriginQueryResult>> Handle(GetDraftOriginQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftOriginQueryResult>>> Handle(GetAllDraftOriginsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}