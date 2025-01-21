using SIMA.Application.Query.Contract.Features.TrustyDrafts.RequestValors;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.RequestValors;

namespace SIMA.Application.Query.Features.TrustyDrafts.RequestValors;

public class RequestValorQueryHandler : IQueryHandler<GetRequestValorQuery, Result<GetRequestValorQueryResult>>,
    IQueryHandler<GetAllRequestValorsQuery, Result<IEnumerable<GetRequestValorQueryResult>>>
{
    private readonly IRequestValorQueryRepository _repository;

    public RequestValorQueryHandler(IRequestValorQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetRequestValorQueryResult>> Handle(GetRequestValorQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetRequestValorQueryResult>>> Handle(GetAllRequestValorsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}