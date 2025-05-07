using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemTypes;

public class ModifyConfigurationItemTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}