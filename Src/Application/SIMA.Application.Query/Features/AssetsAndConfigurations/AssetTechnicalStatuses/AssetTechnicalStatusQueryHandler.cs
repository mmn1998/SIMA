using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTechnicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetTechnicalStatuses;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.AssetTechnicalStatuses;

public class AssetTechnicalStatusQueryHandler : IQueryHandler<GetAssetTechnicalStatusQuery, Result<GetAssetTechnicalStatusQueryResult>>,
    IQueryHandler<GetAllAssetTechnicalStatusesQuery, Result<IEnumerable<GetAssetTechnicalStatusQueryResult>>>
{
    private readonly IAssetTechnicalStatusQueryRepository _repository;

    public AssetTechnicalStatusQueryHandler(IAssetTechnicalStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAssetTechnicalStatusQueryResult>> Handle(GetAssetTechnicalStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAssetTechnicalStatusQueryResult>>> Handle(GetAllAssetTechnicalStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}