using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;

public class GetAssetByIdQuery : IQuery<Result<GetAssetQueryInfoResult>>
{
    public long Id { get; set; }
}
