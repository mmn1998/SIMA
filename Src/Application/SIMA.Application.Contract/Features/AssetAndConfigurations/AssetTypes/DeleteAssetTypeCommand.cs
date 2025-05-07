using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTypes;

public class DeleteAssetTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}