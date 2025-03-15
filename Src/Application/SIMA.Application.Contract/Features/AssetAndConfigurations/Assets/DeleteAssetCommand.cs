using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;

public class DeleteAssetCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}