using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Assets;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.Assets;

public class AssetQueryHandler : IQueryHandler<GetAllAssetsQuery, Result<IEnumerable<GetAssetQueryResult>>>
{
    private readonly IAssetQueryRepository _repository;

    public AssetQueryHandler(IAssetQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetAssetQueryResult>>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}