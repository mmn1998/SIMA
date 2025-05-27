using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetCustomFields;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.AssetCustomFields
{
    public class AssetCustomFieldQueryHandler : IQueryHandler<GetAssetCustomFieldQuery, Result<GetAssetCustomFieldQueryResult>>,
    IQueryHandler<GetAllAssetCustomFieldsQuery, Result<IEnumerable<GetAssetCustomFieldQueryResult>>>
    {
        private readonly IAssetCustomFieldQueryRepository _repository;

        public AssetCustomFieldQueryHandler(IAssetCustomFieldQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetAssetCustomFieldQueryResult>> Handle(GetAssetCustomFieldQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetAssetCustomFieldQueryResult>>> Handle(GetAllAssetCustomFieldsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
