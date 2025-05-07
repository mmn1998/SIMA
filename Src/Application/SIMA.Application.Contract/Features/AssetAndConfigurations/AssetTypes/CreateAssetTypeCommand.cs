using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTypes;

public class CreateAssetTypeCommand : ICommand<Result<long>>
{
    public long? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}