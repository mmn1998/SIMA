using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetPhysicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetPhysicalStatuses;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.AssetPhysicalStatuses;

public class AssetPhysicalStatusQueryHandler : IQueryHandler<GetAssetPhysicalStatusQuery, Result<GetAssetPhysicalStatusQueryResult>>,
    IQueryHandler<GetAllAssetPhysicalStatusesQuery, Result<IEnumerable<GetAssetPhysicalStatusQueryResult>>>
{
    private readonly IAssetPhysicalStatusQueryRepository _repository;

    public AssetPhysicalStatusQueryHandler(IAssetPhysicalStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAssetPhysicalStatusQueryResult>> Handle(GetAssetPhysicalStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAssetPhysicalStatusQueryResult>>> Handle(GetAllAssetPhysicalStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}