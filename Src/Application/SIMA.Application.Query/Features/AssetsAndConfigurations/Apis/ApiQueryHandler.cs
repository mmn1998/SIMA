using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Apis;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.Apis;

public class ApiQueryHandler : IQueryHandler<GetApiQuery, Result<GetApiQueryResult>>,
    IQueryHandler<GetAllApisQuery, Result<IEnumerable<GetApiQueryResult>>>
{
    private readonly IApiQueryRepository _repository;

    public ApiQueryHandler(IApiQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetApiQueryResult>> Handle(GetApiQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetApiQueryResult>>> Handle(GetAllApisQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}