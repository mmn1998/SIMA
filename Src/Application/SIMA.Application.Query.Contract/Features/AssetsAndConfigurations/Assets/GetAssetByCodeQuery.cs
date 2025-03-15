using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;

public class GetAssetByCodeQuery : IQuery<Result<GetAssetQueryInfoResult>>
{
    public string Code { get; set; }
}