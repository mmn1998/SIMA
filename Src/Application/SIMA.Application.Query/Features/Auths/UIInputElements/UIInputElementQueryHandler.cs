using SIMA.Application.Query.Contract.Features.Auths.UIInputElements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.UIInputElements;

namespace SIMA.Application.Query.Features.Auths.UIInputElements;

public class UIInputElementQueryHandler : IQueryHandler<GetUIInputElementQuery, Result<GetUIInputElementQueryResult>>,
    IQueryHandler<GetAllUIInputElementsQuery, Result<IEnumerable<GetUIInputElementQueryResult>>>
{
    private readonly IUIInputElementQueryRepository _repository;

    public UIInputElementQueryHandler(IUIInputElementQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetUIInputElementQueryResult>> Handle(GetUIInputElementQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetUIInputElementQueryResult>>> Handle(GetAllUIInputElementsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}