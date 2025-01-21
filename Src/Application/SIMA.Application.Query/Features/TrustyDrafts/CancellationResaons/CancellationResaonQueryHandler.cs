using SIMA.Application.Query.Contract.Features.TrustyDrafts.CancellationResaons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.CancellationResaons;

namespace SIMA.Application.Query.Features.TrustyDrafts.CancellationResaons;

public class CancellationResaonQueryHandler : IQueryHandler<GetCancellationResaonQuery, Result<GetCancellationResaonQueryResult>>,
    IQueryHandler<GetAllCancellationResaonsQuery, Result<IEnumerable<GetCancellationResaonQueryResult>>>
{
    private readonly ICancellationResaonQueryRepository _repository;

    public CancellationResaonQueryHandler(ICancellationResaonQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCancellationResaonQueryResult>> Handle(GetCancellationResaonQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCancellationResaonQueryResult>>> Handle(GetAllCancellationResaonsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}