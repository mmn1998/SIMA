using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftCurrencyOrigins;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftCurrencyOrigins;

public class DraftCurrencyOriginQueryHandler : IQueryHandler<GetDraftCurrencyOriginQuery, Result<GetDraftCurrencyOriginQueryResult>>,
    IQueryHandler<GetAllDraftCurrencyOriginsQuery, Result<IEnumerable<GetDraftCurrencyOriginQueryResult>>>
{
    private readonly IDraftCurrencyOriginQueryRepository _repository;

    public DraftCurrencyOriginQueryHandler(IDraftCurrencyOriginQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftCurrencyOriginQueryResult>> Handle(GetDraftCurrencyOriginQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftCurrencyOriginQueryResult>>> Handle(GetAllDraftCurrencyOriginsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}