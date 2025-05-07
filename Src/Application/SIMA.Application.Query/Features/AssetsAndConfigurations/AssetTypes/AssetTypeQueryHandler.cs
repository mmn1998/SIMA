using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetTypes;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.AssetTypes;

public class AssetTypeQueryHandler : IQueryHandler<GetAssetTypeQuery, Result<GetAssetTypeQueryResult>>,
    IQueryHandler<GetAllAssetTypesQuery, Result<IEnumerable<GetAssetTypeQueryResult>>>
{
    private readonly IAssetTypeQueryRepository _repository;

    public AssetTypeQueryHandler(IAssetTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAssetTypeQueryResult>> Handle(GetAssetTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAssetTypeQueryResult>>> Handle(GetAllAssetTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}