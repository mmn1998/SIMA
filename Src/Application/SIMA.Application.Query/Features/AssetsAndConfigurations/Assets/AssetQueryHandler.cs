using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Assets;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.Assets;

public class AssetQueryHandler : IQueryHandler<GetAllAssetsQuery, Result<IEnumerable<GetAssetQueryResult>>>,
    IQueryHandler<GetAssetByCodeQuery, Result<GetAssetQueryInfoResult>>,
    IQueryHandler<GetAssetByIdQuery, Result<GetAssetQueryInfoResult>>,
    IQueryHandler<GetAssetComboQuery,Result<IEnumerable<GetAssetComboQueryResult>>>
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

    public async Task<Result<GetAssetQueryInfoResult>> Handle(GetAssetByCodeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCode(request);
    }

    public async Task<Result<GetAssetQueryInfoResult>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAssetComboQueryResult>>> Handle(GetAssetComboQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAssetCombo(request);
    }
}